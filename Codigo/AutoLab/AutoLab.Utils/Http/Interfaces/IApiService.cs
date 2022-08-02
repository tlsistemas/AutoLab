using AutoLab.Utils.Http.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Utils.Http.Interface
{
    public interface IApiService
    {
        BaseResponse MsgError(string BaseResponse = "");
        Task<BaseResponse> Get(string url, string tokenHash = null);
        Task<BaseResponse<T>> Get<T>(string url, string tokenHash = null);
        Task<BaseResponse> Post(string url, string json, string tokenHash = null);
        Task<BaseResponse> Put(string url, string json, string tokenHash = null);
        Task<BaseResponse> Update(string url, string json, string tokenHash = null);
        Task<BaseResponse> Patch(string url, string json, string tokenHash = null);
        Task<BaseResponse> Delete(string url, string tokenHash = null);
        string QueryString(IDictionary<string, object> dict);
        String GetWebRequest(String url);
    }
}
