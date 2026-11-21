using System.Globalization;
using System.Text;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownMetricsModel
    {
        private readonly string[] MetricHeaders =
        {
            "PremiumRequests",
            "PremiumRequestsCost",
            "PremiumRequestsCostInUsd",
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
            builder.AppendLine("SessionStartTime (UTC): " + SessionStartTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture));
            builder.AppendLine("SessionEndTime (UTC): " + SessionEndTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture));
            builder.AppendLine("SessionDuration: " + SessionDuration.ToString("c", CultureInfo.InvariantCulture));
            builder.AppendLine("TotalPremiumRequests: " + FormatNumber(TotalPremiumRequests));
            builder.AppendLine("TotalPremiumRequestsCostInUsd: " + FormatUsd(TotalPremiumRequestsCostInUsd));
            builder.AppendLine("TotalNanoAiu: " + FormatNumber(TotalNanoAiu));
            builder.AppendLine("TotalNanoAiuInUsd: " + FormatUsd(TotalNanoAiuInUsd));
            builder.AppendLine("EstimatedCostInUsd: " + FormatUsd(estimatedCost) + " (" + estimatedCostSource + ")");
            builder.AppendLine();
        }

        private void AppendSessionTable(StringBuilder builder)
        {
            var headers = new[] { "Session" }.Concat(MetricHeaders).ToArray();
            var rows = ModelMetrics.Select(metric => CreateRow(new[] { "Session (" + metric.ModelName + ")" }, metric)).ToList();
            rows.Add(CreateRow(new[] { "Total" }, Sum(ModelMetrics)));
            AppendTable(builder, "💲📊 PER SESSION", headers, rows);
        }

        private void AppendAgentTable(StringBuilder builder)
        {
            var headers = new[] { "Agent", "Model" }.Concat(MetricHeaders).ToArray();
            var agents = AgentMetrics.OrderByDescending(agent => agent.AgentIdentifier == "main").ToList();
            var rows = agents.SelectMany(agent => agent.ModelMetrics.Select(metric => CreateRow(new[] { agent.AgentName + " (" + agent.AgentIdentifier + ")", metric.ModelName }, metric))).ToList();
            rows.Add(CreateRow(new[] { "Total", "-" }, Sum(agents.SelectMany(agent => agent.ModelMetrics))));
            AppendTable(builder, "💲📊 PER AGENT", headers, rows);
        }

        private void AppendModelTable(StringBuilder builder)
        {
            var headers = new[] { "Model" }.Concat(MetricHeaders).ToArray();
            var rows = ModelMetrics.Select(metric => CreateRow(new[] { metric.ModelName }, metric)).ToList();
            rows.Add(CreateRow(new[] { "Total" }, Sum(ModelMetrics)));
            AppendTable(builder, "💲📊 PER MODEL", headers, rows);
        }

        private void AppendTable(StringBuilder builder, string title, string[] headers, List<string[]> rows)
        {
            var widths = headers.Select((header, index) => Math.Max(header.Length, rows.Count == 0 ? 0 : rows.Max(row => row[index].Length))).ToArray();
            var separator = "+" + string.Join("+", widths.Select(width => new string('-', width + 2))) + "+";

            builder.AppendLine(title);
            builder.AppendLine(separator);
            builder.AppendLine(FormatRow(headers, widths));
            builder.AppendLine(separator);

            foreach (var row in rows)
                builder.AppendLine(FormatRow(row, widths));

            builder.AppendLine(separator);
            builder.AppendLine();
        }

        private string FormatRow(string[] values, int[] widths)
        {
            return "| " + string.Join(" | ", values.Select((value, index) => value.PadRight(widths[index]))) + " |";
        }

        private string[] CreateRow(string[] labels, AiModelTokenConsumptionMetricsModel metric)
        {
            return labels.Concat(new[]
            {
                FormatNumber(metric.TotalPremiumRequests),
                FormatNumber(metric.TotalPremiumRequestsCost),
                FormatUsd(metric.TotalPremiumRequestsCostInUsd),
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
                TotalPremiumRequests = values.Sum(metric => metric.TotalPremiumRequests),
                TotalPremiumRequestsCost = values.Sum(metric => metric.TotalPremiumRequestsCost),
                TotalPremiumRequestsCostInUsd = values.Sum(metric => metric.TotalPremiumRequestsCostInUsd),
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
