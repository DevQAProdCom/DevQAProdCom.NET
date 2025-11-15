using Amazon.Runtime;

namespace DevQAProdCom.NET.Cloud.Aws.SecurityTokenService
{
    public interface IAwsSecurityTokenServiceApiService
    {
        Task<AWSCredentials> GetCredentials(string roleArn, string roleSessionName, AWSCredentials credentials = null);
        Task<AWSCredentials> GetCredentials(string profileName, string roleArn, string roleSessionName);
    }
}
