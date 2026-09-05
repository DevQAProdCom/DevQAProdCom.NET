namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    public class OrchestratorReadWriteAgentRequestModel
    {
        public List<string> FilePathsToRead { get; set; } = new List<string>();
        public string OutputFilePathToWrite { get; set; }
    }
}
