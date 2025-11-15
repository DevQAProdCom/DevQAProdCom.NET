using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.Enumerations;

namespace DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces
{
    public interface IBaseLogger : IHaveIdentifiers, IDisposable
    {
        public LogLevel? MinimumLogLevel { get; }

        #region Log Levels Methods

        ///// <summary>
        ///// Verbose: anything and everything you might want to know about a running block of code.
        ///// </summary>
        public void Verbose(string message, params object[]? values);
        public void Verbose(ILogMessage logMessage);

        ///// <summary>
        ///// Debug: internal system events that aren't necessarily observable from the outside.
        ///// </summary>
        public void Debug(string message, params object[]? values);
        public void Debug(ILogMessage logMessage);

        ///// <summary>
        ///// Information: the lifeblood of operational intelligence - things happen.
        ///// </summary>
        public void Info(string message, params object[]? values);
        public void Info(ILogMessage logMessage);

        ///// <summary>
        ///// Warning: service is degraded or endangered.
        ///// </summary>
        public void Warning(string message, params object[]? values);
        public void Warning(ILogMessage logMessage);

        ///// <summary>
        ///// Error: functionality is unavailable, invariants are broken or data is lost.
        ///// </summary>
        public void Error(string message, params object[]? values);
        public void Error(ILogMessage logMessage);

        ///// <summary>
        ///// Fatal: if you have a pager, it goes off when one of these occurs.
        ///// </summary>
        public void Fatal(string message, params object[]? values);
        public void Fatal(ILogMessage logMessage);

        #endregion Log Levels Methods
    }
}
