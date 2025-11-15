namespace DevQAProdCom.NET.API.Models.HttpResponseModels
{
    public class JsonHttpResponseModel<T> : TextHttpResponseModel
    {
        public T TContent { get; set; }
    }
}
