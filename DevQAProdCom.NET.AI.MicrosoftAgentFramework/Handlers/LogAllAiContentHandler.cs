using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers
{
    public class LogAllAiContentHandler(string filePath, ILogger logger) : IAiContentHandler
    {
        private readonly string _filePath = filePath;
        private readonly object _fileInitializationLock = new();
        private int _fileInitialized = 0;

        public virtual void HandleEvent(AIContent content, IAiInteractionDataBank interactionDataBank)
        {
            if (content.RawRepresentation == null)
                return;

            EnsureFileExistsOnce();
            var rawRepresentation = content.RawRepresentation.ToJson();
            IoUtils.AppendAllText(_filePath, rawRepresentation + Environment.NewLine);
        }

        private void EnsureFileExistsOnce()
        {
            if (_fileInitialized == 1)
                return;

            lock (_fileInitializationLock)
            {
                if (_fileInitialized == 1)
                    return;

                if (!IoUtils.FileExists(_filePath))
                    IoUtils.WriteAllText(_filePath, string.Empty);

                _fileInitialized = 1;
            }
        }

        public virtual void Finally()
        {

        }
    }
}
