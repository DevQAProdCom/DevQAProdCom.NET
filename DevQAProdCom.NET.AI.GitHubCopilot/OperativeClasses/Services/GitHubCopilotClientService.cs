using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.GitHubCopilot.Handlers;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Services
{
    public class GitHubCopilotClientService : IGitHubCopilotClientService
    {
        private CopilotClientOptionsBuilder? _copilotClientOptionsBuilder;
        private CopilotClientOptions? _copilotClientOptions;
        private CopilotClient? _copilotClient;
        private readonly List<SessionEventHandler> _gitHubCopilotSessionEventHandlers = new();
        private ILogger _logger;

        public GitHubCopilotClientService(ILogger logger)
        {
            _logger = logger;
        }

        public GitHubCopilotClientService(ILogger logger, CopilotClientOptions copilotClientOptions) : this(logger)
        {
            _copilotClientOptions = copilotClientOptions;
        }

        public GitHubCopilotClientService WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            _copilotClientOptionsBuilder ??= new CopilotClientOptionsBuilder(_logger);
            updateCopilotClientOptionsFunc(_copilotClientOptionsBuilder);
            return this;
        }

        public GitHubCopilotClientService WithGitHubCopilotSessionEventHandlers(params SessionEventHandler[] handlers)
        {
            if (handlers?.Length > 0)
                _gitHubCopilotSessionEventHandlers.AddRange(handlers);

            return this;
        }

        public CopilotClient GetGitHubCopilotClient(CopilotClientOptions? copilotClientOptions = null)
        {
            if (_copilotClient == null)
            {
                if (copilotClientOptions != null)
                    _copilotClientOptions = copilotClientOptions;
                else
                    _copilotClientOptions = GetCopilotClientOptions();

                _copilotClient = new CopilotClient(_copilotClientOptions);
            }

            return _copilotClient;
        }

        public CopilotClientOptions GetCopilotClientOptions()
        {
            //Set directly without Builder
            if (_copilotClientOptions != null)
                return _copilotClientOptions;

            //Set using Builder
            if (_copilotClientOptionsBuilder != null)
            {
                return _copilotClientOptionsBuilder.Build();
            }

            //Default
            return new CopilotClientOptions();
        }

        public async ValueTask DisposeAsync()
        {
            if (_copilotClient != null)
            {
                var sessions = await _copilotClient.Rpc.Sessions.ListAsync();

                foreach (var session in sessions.Sessions)
                {
                    await _copilotClient.Rpc.Sessions.CloseAsync(session.SessionId);

                    var persistedEvents = await _copilotClient.Rpc.Sessions.ReadPersistedEventsAsync(
                        session.SessionId,
                        max: 20,
                        direction: EventsReadDirection.Backward);

                    var interactionDataBank = new AiInteractionDataBank();

                    foreach (var persistedEvent in persistedEvents.Events.Reverse())
                    {
                        var rawEvent = persistedEvent.ToJson();

                        foreach (var handler in _gitHubCopilotSessionEventHandlers.Where(x=>x.IsDisposeTimeProcessing))
                            handler.HandleEvent(rawEvent, interactionDataBank);
                    }

                    foreach (var handler in _gitHubCopilotSessionEventHandlers)
                        handler.Finally();
                }

                await _copilotClient.DisposeAsync();
                _copilotClient = null;
            }

            if (!string.IsNullOrEmpty(_copilotClientOptions?.BaseDirectory))
            {
                Directory.Delete(_copilotClientOptions.BaseDirectory, recursive: true);
            }
        }
    }
}
