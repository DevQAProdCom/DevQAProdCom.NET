namespace DevQAProdCom.NET.API.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpResponseMessage Send(this HttpRequestMessage requestMessage, TimeSpan? timeout = null)
        {
            timeout ??= TimeSpan.FromSeconds(30);
            HttpResponseMessage responseMessage = null;

            if (requestMessage != null)
            {
                using (var client = new HttpClient() { Timeout = timeout.Value })
                {
                    try
                    {
                        responseMessage = client.Send(requestMessage);
                    }
                    catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
                    {
                        throw new TimeoutException(
                            requestMessage.Method + " request to the endpoint '" + requestMessage.RequestUri.AbsoluteUri + "' has timed out." + "\nException message: '" + e.Message + "'");
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"'{e.GetType()}' appeared while making " + requestMessage.Method + " request to the endpoint '" +
                                            requestMessage.RequestUri.AbsoluteUri + "'." + "\nException message: '" + e.Message + "'");
                    }
                }
            }

            return responseMessage;
        }
    }
}
