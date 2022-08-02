using AutoLab.Core.Models.Response;
using System;
using System.Threading.Tasks;

namespace AutoLab.Services.Interfaces
{
    public interface IApiService
    {
        BaseResponse MsgError(string BaseResponse = "");
        Task<string> GetRestAsync(string url);
        Task<string> GetClientAsync(string url);
    }
}
