using AutoLab.Utils.Http.Response;

namespace AutoLab.Utils.Interfaces
{
    public interface IApiService
    {
        BaseResponse MsgError(string BaseResponse = "");
        Task<string> GetRestAsync(string url);
        Task<string> GetClientAsync(string url);
    }
}
