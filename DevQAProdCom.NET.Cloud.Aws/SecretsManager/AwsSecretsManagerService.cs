using Amazon;
using DevQAProdCom.NET.Cloud.Aws.SecurityTokenService;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.Cloud.Aws.SecretsManager
{
    public class AwsSecretsManagerService : IGetSecret
    {
        private readonly IAwsSecurityTokenServiceApiService _securityTokenApiService;
        private readonly IAwsSecretsManagerApiService _secretsManagerApiService;

        public AwsSecretsManagerService(IAwsSecretsManagerApiService secretsManagerApiService, IAwsSecurityTokenServiceApiService securityTokenApiService = null)
        {
            _secretsManagerApiService = secretsManagerApiService;
            _securityTokenApiService = securityTokenApiService;
        }

        public async Task<T> GetSecret<T>(string secretName) where T : class
        {
            return await _secretsManagerApiService.GetSecret<T>(secretName);
        }

        public async Task<T> GetSecretWithAssumedRole<T>(string secretName, string region = null, string roleArn = null, string roleSessionName = null) where T : class
        {
            var credentials = await _securityTokenApiService.GetCredentials(roleArn, roleSessionName);
            return await _secretsManagerApiService.GetSecret<T>(secretName, RegionEndpoint.GetBySystemName(region), credentials);
        }
    }
}
