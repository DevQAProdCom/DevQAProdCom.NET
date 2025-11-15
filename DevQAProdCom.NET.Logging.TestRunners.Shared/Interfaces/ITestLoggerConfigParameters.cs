namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces
{
    public interface ITestLoggerConfigParameters
    {
        #region General Test Run Info

        public string TestRunId { get; set; }
        public string? TestRunName { get; set; }
        public string? TestRunDescription { get; set; }
        public string? VersionUnderTest { get; set; }

        #endregion General Test Run Info

        public List<string> WriteMode { get; set; }
    }
}
