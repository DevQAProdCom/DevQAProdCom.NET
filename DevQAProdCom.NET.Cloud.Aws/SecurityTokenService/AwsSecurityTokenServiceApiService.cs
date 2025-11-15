using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

namespace DevQAProdCom.NET.Cloud.Aws.SecurityTokenService
{
    public class AwsSecurityTokenServiceApiService : IAwsSecurityTokenServiceApiService
    {
        private readonly AwsCredentialsService? _awsCredentialsService;
        private readonly string? _profileName;
        private readonly AWSCredentials? _credentials;
        private readonly RegionEndpoint? _region;

        public AwsSecurityTokenServiceApiService(AwsCredentialsService awsCredentialsService, string? profileName = null, AWSCredentials? credentials = null, RegionEndpoint? region = null)
        {
            _awsCredentialsService = awsCredentialsService;
            _profileName = profileName;
            _credentials = credentials;
            _region = region;
        }

        public async Task<AWSCredentials> GetCredentials(string roleArn, string roleSessionName, AWSCredentials credentials = null)
        {
            if (string.IsNullOrEmpty(roleArn))
                throw new ArgumentException("Role ARN should not be null or empty.");

            if (string.IsNullOrEmpty(roleSessionName))
                throw new ArgumentException("Role Session Name should not be null or empty.");

            credentials ??= _credentials;
            credentials ??= _awsCredentialsService.GetCredentials();

            if (credentials == null)
                throw new ArgumentException("AWS Credentials should be provided.");

            using (var client = new AmazonSecurityTokenServiceClient(credentials, _region))
            {
                var request = new AssumeRoleRequest() { RoleArn = roleArn, RoleSessionName = roleSessionName };
                var response = await client.AssumeRoleAsync(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                    return response.Credentials;

                throw new HttpRequestException($"HTTP Status Code '{response.HttpStatusCode}' was returned while retrieving temporary credentials in AWS Security Token Service" +
                                               $" for Role ARN '{roleArn}' and Role Session Name '{roleSessionName}'.");
            }
        }

        public async Task<AWSCredentials> GetCredentials(string profileName, string roleArn, string roleSessionName)
        {
            profileName = string.IsNullOrEmpty(profileName) ? _profileName : profileName;
            var credentials = _awsCredentialsService.GetCredentials(profileName);

            return await GetCredentials(roleArn, roleSessionName, credentials);
        }
    }
}
