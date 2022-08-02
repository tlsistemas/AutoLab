using AutoLab.Core.Models.Response;
using AutoLab.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoLab.Services
{
    public class ApiService : IApiService
    {
        public const string ContentType = "application/json";
        const long MaxBufferSize = 256000;       

        public async Task<string> GetRestAsync(string url)
        {
            try
            {
                var options = new RestClientOptions(url)
                {
                    ThrowOnAnyError = true,
                    Timeout = 30000
                };
                var client = new RestClient(options);
                var request = new RestRequest("",Method.Get);
                var response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<string> GetClientAsync(string url)
        {
            try
            {
                var uri = new Uri(url);
                HttpClient myClient = new HttpClient();

                var response = await myClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else return "";
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public BaseResponse MsgError(string code, string message)
        {
            return new BaseResponse(code, message);
        }
        public BaseResponse MsgError(string BaseResponse)
        {
            try
            {
                if (BaseResponse != "")
                {
                    var BaseResponseError = JsonConvert.DeserializeObject<BaseResponse>(BaseResponse);
                    return new BaseResponse(BaseResponseError.Messages);
                }
                else
                {
                    return new BaseResponse();
                }
            }
            catch (Exception ex)
            {
                return MsgError(ex.GetType().Name, ex.Message);
            }
        }
        public BaseResponse<T> MsgError<T>(string BaseResponse)
        {
            try
            {
                if (BaseResponse != "")
                {
                    var BaseResponseError = JsonConvert.DeserializeObject<BaseResponse>(BaseResponse);
                    return new BaseResponse<T>(BaseResponseError.Messages);
                }
                else
                {
                    return new BaseResponse<T>();
                }
            }
            catch (Exception ex)
            {
                return MsgError<T>(ex.GetType().Name, ex.Message);
            }
        }
        public BaseResponse<T> MsgError<T>(string code, string message)
        {
            return new BaseResponse<T>(code, message);
        }


    }
}
