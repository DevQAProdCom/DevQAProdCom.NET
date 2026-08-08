using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses
{
    /// <summary>
    /// An interactor that configures and runs a GitHub Copilot agent through the
    /// Microsoft Agents AI framework.
    /// </summary>
    public class GitHubCopilotAiAgentInteractor : MicrosoftAiAgentInteractor
    {
        private readonly SessionConfigBuilder _sessionConfigBuilder;
        private CopilotClient? _copilotClient;
        private readonly CopilotClientOptionsBuilder _copilotClientOptionsBuilder = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubCopilotAiAgentInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logger used to emit diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is <see langword="null"/>.</exception>
        public GitHubCopilotAiAgentInteractor(ILogger logger) : base(logger)
        {
            _sessionConfigBuilder = new SessionConfigBuilder(logger);

            var handler = new GitHubCopilotAssistantMessageEventAiContentHandler(logger);
            WithAiContentHandlers(handler);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubCopilotAiAgentInteractor"/> class
        /// and selects the specified agent.
        /// </summary>
        /// <param name="agentIdentifier">The identifier of the agent to activate.</param>
        /// <param name="logger">The logger used to emit diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="agentIdentifier"/> or <paramref name="logger"/> is <see langword="null"/>.</exception>
        public GitHubCopilotAiAgentInteractor(string agentIdentifier, ILogger logger) : this(logger)
        {
            WithAgent(agentIdentifier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubCopilotAiAgentInteractor"/> class,
        /// selects the specified agent, and sets the working directory.
        /// </summary>
        /// <param name="agentIdentifier">The identifier of the agent to activate.</param>
        /// <param name="workingDirectory">The working directory for the session.</param>
        /// <param name="logger">The logger used to emit diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="agentIdentifier"/>, <paramref name="workingDirectory"/>, or <paramref name="logger"/> is <see langword="null"/>.</exception>
        public GitHubCopilotAiAgentInteractor(string agentIdentifier, string workingDirectory, ILogger logger) : this(agentIdentifier, logger)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubCopilotAiAgentInteractor"/> class
        /// and sets the working directory.
        /// </summary>
        /// <param name="agentMdFilePath">The file containing the agent YAML configuration.</param>
        /// <param name="workingDirectory">The working directory for the session.</param>
        /// <param name="logger">The logger used to emit diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="agentMdFilePath"/>, <paramref name="workingDirectory"/>, or <paramref name="logger"/> is <see langword="null"/>.</exception>
        public GitHubCopilotAiAgentInteractor(FileInfo agentMdFilePath, string workingDirectory, ILogger logger) : this(logger)
        {
            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.Agent"/> parameter to the specified agent identifier.
        /// </summary>
        /// <param name="agentIdentifier">The name of the custom agent to activate.</param>
        /// <returns>The current <see cref="GitHubCopilotAiAgentInteractor"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="agentIdentifier"/> is <see langword="null"/>.</exception>
        public override GitHubCopilotAiAgentInteractor WithAgent(string agentIdentifier)
        {
            ArgumentNullException.ThrowIfNull(agentIdentifier);

            _sessionConfigBuilder.WithAgent(agentIdentifier);
            return this;
        }

        /// <summary>
        /// Loads a custom agent from the specified file and sets the
        /// <see cref="SessionConfigBase.Agent"/> parameter to the agent's name.
        /// </summary>
        /// <param name="agentMdFilePath">The file containing the agent YAML configuration.</param>
        /// <returns>The current <see cref="GitHubCopilotAiAgentInteractor"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="agentMdFilePath"/> is <see langword="null"/>.</exception>
        public override GitHubCopilotAiAgentInteractor WithAgent(FileInfo agentMdFilePath)
        {
            ArgumentNullException.ThrowIfNull(agentMdFilePath);

            _sessionConfigBuilder.WithAgent(agentMdFilePath);
            return this;
        }

        /// <summary>
        /// Sets the working directory for the Copilot client.
        /// </summary>
        /// <param name="workingDirectory">The working directory for the session.</param>
        /// <returns>The current <see cref="GitHubCopilotAiAgentInteractor"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="workingDirectory"/> is <see langword="null"/>.</exception>
        public override GitHubCopilotAiAgentInteractor WithWorkingDirectory(string workingDirectory)
        {
            ArgumentNullException.ThrowIfNull(workingDirectory);

            _copilotClientOptionsBuilder.WithWorkingDirectory(workingDirectory);
            return this;
        }

        /// <summary>
        /// Applies a custom transformation to the underlying <see cref="SessionConfigBuilder"/>.
        /// </summary>
        /// <param name="updateSessionConfigFunc">A delegate that configures the session config builder.</param>
        /// <returns>The current <see cref="GitHubCopilotAiAgentInteractor"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateSessionConfigFunc"/> is <see langword="null"/>.</exception>
        public GitHubCopilotAiAgentInteractor WithSessionConfig(Func<SessionConfigBuilder, SessionConfigBuilder> updateSessionConfigFunc)
        {
            ArgumentNullException.ThrowIfNull(updateSessionConfigFunc);

            updateSessionConfigFunc(_sessionConfigBuilder);
            return this;
        }

        /// <summary>
        /// Applies a custom transformation to the underlying <see cref="CopilotClientOptionsBuilder"/>.
        /// </summary>
        /// <param name="updateCopilotClientOptionsFunc">A delegate that configures the client options builder.</param>
        /// <returns>The current <see cref="GitHubCopilotAiAgentInteractor"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateCopilotClientOptionsFunc"/> is <see langword="null"/>.</exception>
        public GitHubCopilotAiAgentInteractor WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc)
        {
            ArgumentNullException.ThrowIfNull(updateCopilotClientOptionsFunc);

            updateCopilotClientOptionsFunc(_copilotClientOptionsBuilder);
            return this;
        }

        /// <summary>
        /// Applies isolation-safe defaults for multi-user or server scenarios.
        /// <para>
        /// This sets <see cref="CopilotClientOptions.Mode"/> to <see cref="CopilotClientMode.Empty"/>
        /// and applies session-level isolation via <see cref="SessionConfigBuilder.WithIsolation"/>.
        /// When <paramref name="baseDirectory"/> is <see langword="null"/>, a default directory is created
        /// under <see cref="Environment.CurrentDirectory"/> using the pattern
        /// <c>COPILOT_SESSSION_BASE_DIR_yyyy-MM-ddThh-mm-ss-ffffff</c>.
        /// </para>
        /// <para>
        /// When <see cref="CopilotClientMode.Empty"/> is selected:
        /// </para>
        /// <list type="bullet">
        /// <item>
        ///     <description>
        ///     The client constructor requires <see cref="CopilotClientOptions.BaseDirectory"/> or
        ///     <see cref="CopilotClientOptions.SessionFs"/> to be set.
        ///     </description>
        /// </item>
        /// <item>
        ///     <description>
        ///     <see cref="SessionConfigBase.AvailableTools"/> must be supplied on every session —
        ///     no tools are exposed by default.
        ///     </description>
        /// </item>
        /// <item>
        ///     <description>
        ///     <c>session.create</c> always sets <c>toolFilterPrecedence: "excluded"</c> so the
        ///     allowlist and denylist compose naturally.
        ///     </description>
        /// </item>
        /// <item>
        ///     <description>
        ///     The SDK injects safe defaults for ambient session features (telemetry, custom
        ///     instructions, plugins, environment context, etc.).
        ///     </description>
        /// </item>
        /// <item>
        ///     <description>
        ///     <c>COPILOT_DISABLE_KEYTAR=1</c> is set on the spawned runtime so credentials are
        ///     persisted to <c>COPILOT_HOME</c> rather than a process-wide system keychain.
        ///     </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="baseDirectory">
        /// The base directory for the isolated Copilot client. When <see langword="null"/>,
        /// defaults to <c>CurrentDirectory\COPILOT_SESSSION_BASE_DIR_yyyy-MM-ddThh-mm-ss-ffffff</c>.
        /// </param>
        /// <returns>The current <see cref="GitHubCopilotAiAgentInteractor"/> instance.</returns>
        public GitHubCopilotAiAgentInteractor WithIsolation(string? baseDirectory = null)
        {
            baseDirectory ??= Path.Combine(
                Environment.CurrentDirectory,
                $"COPILOT_SESSSION_BASE_DIR_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}");

            _copilotClientOptionsBuilder.WithBaseDirectory(baseDirectory);
            _sessionConfigBuilder.WithIsolation();
            _copilotClientOptionsBuilder.WithMode(CopilotClientMode.Empty);
            return this;
        }

        /// <summary>
        /// Builds and starts the underlying <see cref="AIAgent"/> instance.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A configured <see cref="AIAgent"/> instance.</returns>
        protected override async Task<AIAgent> BuildAiAgentAsync(CancellationToken cancellationToken = default)
        {
            if (AiAgent != null)
            {
                return AiAgent;
            }

            var copilotClientOptions = _copilotClientOptionsBuilder.Build();
            _copilotClient = new CopilotClient(copilotClientOptions);
            await _copilotClient.StartAsync(cancellationToken);

            var sessionConfig = _sessionConfigBuilder.Build();
            AIAgent agent = _copilotClient.AsAIAgent(sessionConfig, ownsClient: true);
            return agent;
        }

        /// <summary>
        /// Disposes the underlying <see cref="CopilotClient"/> if it has been created.
        /// </summary>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        public override async ValueTask DisposeAsync()
        {
            if (_copilotClient != null)
            {
                await _copilotClient.DisposeAsync();
                _copilotClient = null;
            }
        }
    }
}
