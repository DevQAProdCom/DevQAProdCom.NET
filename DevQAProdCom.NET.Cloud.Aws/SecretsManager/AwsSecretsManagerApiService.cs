using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace DevQAProdCom.NET.Cloud.Aws.SecretsManager
{
    public class AwsSecretsManagerApiService : IAwsSecretsManagerApiService
    {
        private readonly RegionEndpoint _region;
        private readonly AWSCredentials _credentials;

        public AwsSecretsManagerApiService(RegionEndpoint region, AWSCredentials credentials)
        {
            _region = region;
            _credentials = credentials;
        }

        public async Task<T> GetSecret<T>(string secretName, RegionEndpoint region = null, AWSCredentials credentials = null) where T : class
        {
            if (string.IsNullOrEmpty(secretName))
                throw new ArgumentException("Secret Name should not be null or empty.");

            credentials = credentials ?? _credentials;
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials), "AWS Credentials should be provided.");

            region = region ?? _region;
            if (region == null)
                throw new ArgumentNullException(nameof(region), "Region should be provided.");

            try
            {
                using (var client = new AmazonSecretsManagerClient(credentials, region))
                {
                    var request = new GetSecretValueRequest() { SecretId = secretName };
                    var response = await client.GetSecretValueAsync(request);

                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        if (typeof(T) == typeof(string))
                            return response.SecretString as T;
                        return JsonConvert.DeserializeObject<T>(response.SecretString);
                    }

                    throw new HttpRequestException($"HTTP Status Code '{response.HttpStatusCode}' was returned while retrieving the secret '{secretName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the secret '{secretName}'. Error message: {ex.Message}");
                throw;
            }
        }
    }
}
