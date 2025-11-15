using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace DevQAProdCom.NET.Cloud.Aws
{
    public class AwsCredentialsService
    {
        public AWSCredentials GetCredentials(string profileName = "default", string filePath = null)
        {
            var sharedFile = new SharedCredentialsFile(filePath);

            if (!sharedFile.TryGetProfile(profileName, out var profile))
                throw new ArgumentException($"Profile '{profileName}' was not found.");

            var credentials = profile.GetAWSCredentials(sharedFile).GetCredentials();

            return new SessionAWSCredentials(credentials.AccessKey, credentials.SecretKey, credentials.Token);
        }
    }
}
