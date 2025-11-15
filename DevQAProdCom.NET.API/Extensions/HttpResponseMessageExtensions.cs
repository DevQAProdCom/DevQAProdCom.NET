using System.Net;
using System.Net.Http.Json;
using DevQAProdCom.NET.API.Models.HttpResponseModels;

namespace DevQAProdCom.NET.API.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessage EnsureStatusCode(this HttpResponseMessage responseMessage, HttpStatusCode? statusCode)
        {
            if (responseMessage == null)
                throw new ArgumentNullException("Unable to ensure status code, because response message is null.");

            if (statusCode != null && responseMessage.StatusCode != statusCode)
                throw new HttpRequestException($"\nExpected status code for the request '{responseMessage.RequestMessage.Method} {responseMessage.RequestMessage.RequestUri.AbsoluteUri}' is: {statusCode}." +
                                               $" \nActual status code: {responseMessage.StatusCode}. \nReason: {responseMessage.ReasonPhrase}.");

            return responseMessage;
        }

        public static TextHttpResponseModel ToTextHttpResponseModel(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                return null;

            var textHttpResponseModel = new TextHttpResponseModel();
            textHttpResponseModel.ResponseMessage = responseMessage;

            if (responseMessage.Content != null)
                textHttpResponseModel.TextContent = responseMessage.Content.ReadAsStringAsync().Result;

            return textHttpResponseModel;
        }

        public static JsonHttpResponseModel<T> ToJsonHttpResponseModel<T>(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                return null;

            var jsonHttpResponseModel = new JsonHttpResponseModel<T>();
            jsonHttpResponseModel.ResponseMessage = responseMessage;

            if (responseMessage.Content != null)
            {
                jsonHttpResponseModel.TextContent = responseMessage.Content.ReadAsStringAsync().Result;
                jsonHttpResponseModel.TContent = responseMessage.Content.ReadFromJsonAsync<T>().Result;
            }

            return jsonHttpResponseModel;
        }
    }
}
