using System.Globalization;
using System.Text;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownMetricsModel
    {
        private readonly string[] MetricIcons =
        {
            "🔢",
            "💰",
            "💲",
            "💰",
            "💲",
            "🪙",
            "🪙",
            "🪙",
            "🪙",
            "🪙"
        };

        private readonly int[] MetricIconOffsets =
        {
            4,
            4,
            5,
            0,
            2,
            0,
            1,
            2,
            1,
            0
        };

        private readonly string[] MetricHeaders =
        {
            "RequestsCount",
            "RequestsCost",
            "RequestsCostInUsd",
            "NanoAiu",
            "NanoAiuInUsd",
            "InputTokens",
            "OutputTokens",
            "CacheReadTokens",
            "CacheWriteTokens",
            "ReasoningTokens"
        };

        public double TotalPremiumRequests { get; set; }
        public decimal TotalPremiumRequestsCostInUsd { get; set; }
        public double TotalNanoAiu { get; set; }
        public decimal TotalNanoAiuInUsd { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionEndTime { get; set; }
        public TimeSpan SessionDuration { get; set; }
        public List<AiModelTokenConsumptionMetricsModel> ModelMetrics { get; set; } = new();
        public List<AiAgentTokenConsumptionMetricsModel> AgentMetrics { get; set; } = new();

        public override string ToString()
        {
            var builder = new StringBuilder();
            AppendGeneralSessionInfo(builder);
            AppendSessionTable(builder);
            AppendAgentTable(builder);
            AppendModelTable(builder);
            return builder.ToString().TrimEnd();
        }

        private void AppendGeneralSessionInfo(StringBuilder builder)
        {
            var useNanoAiu = TotalNanoAiu > 0;
            var estimatedCost = useNanoAiu ? TotalNanoAiuInUsd : TotalPremiumRequestsCostInUsd;
            var estimatedCostSource = useNanoAiu ? "NanoAiu" : "PremiumRequests";

            builder.AppendLine("💲📊 GENERAL SESSION INFO");
            builder.AppendLine("📅 SessionStartTime (UTC): " + SessionStartTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture));
            builder.AppendLine("📅 SessionEndTime (UTC): " + SessionEndTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture));
            builder.AppendLine("⏱️ SessionDuration: " + SessionDuration.ToString("c", CultureInfo.InvariantCulture));
            builder.AppendLine("💰 TotalPremiumRequests: " + FormatNumber(TotalPremiumRequests));
            builder.AppendLine("💵 TotalPremiumRequestsCostInUsd: " + FormatUsd(TotalPremiumRequestsCostInUsd));
            builder.AppendLine("💰 TotalNanoAiu: " + FormatNumber(TotalNanoAiu));
            builder.AppendLine("💵 TotalNanoAiuInUsd: " + FormatUsd(TotalNanoAiuInUsd));
            builder.AppendLine("💵 EstimatedCostInUsd: " + FormatUsd(estimatedCost) + " (" + estimatedCostSource + ")");
            builder.AppendLine();
        }

        private void AppendSessionTable(StringBuilder builder)
        {
            var icons = new[] { "💲📊💬" }.Concat(MetricIcons).ToArray();
            var headers = new[] { "PER SESSION" }.Concat(MetricHeaders).ToArray();
            var rows = ModelMetrics.Select(metric => CreateRow(new[] { "Session (" + metric.ModelName + ")" }, metric)).ToList();
            rows.Add(CreateRow(new[] { "TOTAL" }, Sum(ModelMetrics)));
            AppendTable(builder, icons, headers, rows);
        }

        private void AppendAgentTable(StringBuilder builder)
        {
            var icons = new[] { "💲📊🤖", "🧠" }.Concat(MetricIcons).ToArray();
            var headers = new[] { "PER AGENT", "Model" }.Concat(MetricHeaders).ToArray();
            var agents = AgentMetrics.OrderByDescending(agent => agent.AgentIdentifier == "main").ToList();
            var rows = agents.SelectMany(agent => agent.ModelMetrics.Select(metric => CreateRow(new[] { agent.AgentName + " (" + agent.AgentIdentifier + ")", metric.ModelName }, metric))).ToList();
            rows.Add(CreateRow(new[] { "TOTAL", "-" }, Sum(agents.SelectMany(agent => agent.ModelMetrics))));
            AppendTable(builder, icons, headers, rows);
        }

        private void AppendModelTable(StringBuilder builder)
        {
            var icons = new[] { "💲📊🧠" }.Concat(MetricIcons).ToArray();
            var headers = new[] { "PER MODEL" }.Concat(MetricHeaders).ToArray();
            var rows = ModelMetrics.Select(metric => CreateRow(new[] { metric.ModelName }, metric)).ToList();
            rows.Add(CreateRow(new[] { "TOTAL" }, Sum(ModelMetrics)));
            AppendTable(builder, icons, headers, rows);
        }

        private void AppendTable(StringBuilder builder, string[] icons, string[] headers, List<string[]> rows)
        {
            var widths = headers.Select((header, index) => Math.Max(GetDisplayWidth(header), rows.Count == 0 ? 0 : rows.Max(row => GetDisplayWidth(row[index])))).ToArray();
            var separator = "+" + string.Join("+", widths.Select(width => new string('-', width + 2))) + "+";

            builder.AppendLine(separator);
            builder.AppendLine(FormatIconRow(icons, widths));
            builder.AppendLine(separator);
            builder.AppendLine(FormatRow(headers, widths, true));
            builder.AppendLine(separator);

            for (var index = 0; index < rows.Count; index++)
            {
                if (index == rows.Count - 1 && index > 0)
                    builder.AppendLine(separator);

                builder.AppendLine(FormatRow(rows[index], widths));
            }

            builder.AppendLine(separator);
            builder.AppendLine();
        }

        private string FormatIconRow(string[] icons, int[] widths)
        {
            var labelColumnsCount = icons.Length - MetricIconOffsets.Length;
            return (" " + string.Join(" ", icons.Select((icon, index) =>
            {
                var cellWidth = widths[index] + 2;
                var offset = index < labelColumnsCount
                    ? Math.Max(0, (cellWidth - GetDisplayWidth(icon)) / 2 - 4)
                    : MetricIconOffsets[index - labelColumnsCount];
                return new string(' ', offset) + icon + new string(' ', Math.Max(0, cellWidth - offset - GetDisplayWidth(icon)));
            }))).TrimEnd();
        }

        private string FormatRow(string[] values, int[] widths, bool centerFirstValue = false)
        {
            return "| " + string.Join(" | ", values.Select((value, index) => centerFirstValue && index == 0
                ? Center(value, widths[index])
                : value + new string(' ', Math.Max(0, widths[index] - GetDisplayWidth(value))))) + " |";
        }

        private string Center(string value, int width)
        {
            var padding = Math.Max(0, width - GetDisplayWidth(value));
            var leftPadding = Math.Max(0, padding / 2 - 2);
            return new string(' ', leftPadding) + value + new string(' ', padding - leftPadding);
        }

        private int GetDisplayWidth(string value)
        {
            return value.EnumerateRunes().Sum(rune => rune.Value is >= 0x1F300 and <= 0x1FAFF ? 2 : 1);
        }

        private string[] CreateRow(string[] labels, AiModelTokenConsumptionMetricsModel metric)
        {
            return labels.Concat(new[]
            {
                FormatNumber(metric.RequestsCount),
                FormatNumber(metric.RequestsCost),
                FormatUsd(metric.RequestsCostInUsd),
                FormatNumber(metric.TotalNanoAiu),
                FormatUsd(metric.TotalNanoAiuInUsd),
                metric.InputTokens.ToString(CultureInfo.InvariantCulture),
                metric.OutputTokens.ToString(CultureInfo.InvariantCulture),
                metric.CacheReadTokens.ToString(CultureInfo.InvariantCulture),
                metric.CacheWriteTokens.ToString(CultureInfo.InvariantCulture),
                metric.ReasoningTokens.ToString(CultureInfo.InvariantCulture)
            }).ToArray();
        }

        private AiModelTokenConsumptionMetricsModel Sum(IEnumerable<AiModelTokenConsumptionMetricsModel> metrics)
        {
            var values = metrics.ToList();
            return new AiModelTokenConsumptionMetricsModel
            {
                ModelName = "Total",
                RequestsCount = values.Sum(metric => metric.RequestsCount),
                RequestsCost = values.Sum(metric => metric.RequestsCost),
                RequestsCostInUsd = values.Sum(metric => metric.RequestsCostInUsd),
                TotalNanoAiu = values.Sum(metric => metric.TotalNanoAiu),
                TotalNanoAiuInUsd = values.Sum(metric => metric.TotalNanoAiuInUsd),
                InputTokens = values.Sum(metric => metric.InputTokens),
                OutputTokens = values.Sum(metric => metric.OutputTokens),
                CacheReadTokens = values.Sum(metric => metric.CacheReadTokens),
                CacheWriteTokens = values.Sum(metric => metric.CacheWriteTokens),
                ReasoningTokens = values.Sum(metric => metric.ReasoningTokens)
            };
        }

        private string FormatNumber(double value)
        {
            return value.ToString("0.#########", CultureInfo.InvariantCulture);
        }

        private string FormatUsd(decimal value)
        {
            return "$" + value.ToString("0.0000", CultureInfo.InvariantCulture);
        }
    }
}
