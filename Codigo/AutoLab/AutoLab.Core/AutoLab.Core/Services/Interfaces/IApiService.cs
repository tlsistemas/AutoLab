using AutoLab.Core.Models.Response;
using System;
using System.Threading.Tasks;

namespace AutoLab.Services.Interfaces
{
    public interface IApiService
    {
        BaseResponse MsgError(string BaseResponse = "");
        Task<BaseResponse<T>> Get<T>(string url);
        Task<String> Get(string url);
        Task<String>  Get(string url, string tokenHash);
        Task<string> GetClientAsync(string url);
        Task<string> GetRestAsync(string url);
    }
}
