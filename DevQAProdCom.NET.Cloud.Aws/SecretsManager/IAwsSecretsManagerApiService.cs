using Amazon;
using Amazon.Runtime;

namespace DevQAProdCom.NET.Cloud.Aws.SecretsManager
{
    public interface IAwsSecretsManagerApiService
    {
        Task<T> GetSecret<T>(string secretName, RegionEndpoint? regionEndpoint = null, AWSCredentials credentials = null) where T : class;
    }
}
