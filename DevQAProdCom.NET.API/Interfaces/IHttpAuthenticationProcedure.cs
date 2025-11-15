namespace DevQAProdCom.NET.API.Interfaces
{
    public interface IHttpAuthenticationProcedure
    {
        public IHttpAuthParameters GetHttpAuthenticationParameters(IAuthParameters authParameters);
    }
}
