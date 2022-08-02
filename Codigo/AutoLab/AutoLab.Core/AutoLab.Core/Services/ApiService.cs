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

        public async Task<BaseResponse<T>> Get<T>(string url)
        {
            string json = "";
            try
            {
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.UserAgent = ".NET Framework Test Client";
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    json = objResponse.ToString();
                    streamDados.Close();
                    resposta.Close();
                }
                var res = JsonConvert.DeserializeObject<T>(json);
                var BaseResponseApi = new BaseResponse<T>();
                BaseResponseApi.Data = res;
                return BaseResponseApi;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError<T>(ex.GetType().Name, ex.Message);
            }


        }

        public async Task<string> Get(string url)
        {
            string json = "";
            try
            {
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.UserAgent = ".NET Framework Test Client";
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    json = objResponse.ToString();
                    streamDados.Close();
                    resposta.Close();
                }
                return json;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }

        }

        public async Task<string> Get(string url, string tokenHash)
        {
            string json = "";
            try
            {
                var handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Framework Test Client");
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("x-glb-token", tokenHash);
                var response = httpClient.SendAsync(request).Result;

                Stream streamResponse = response.Content.ReadAsStreamAsync().Result;
                StreamReader streamRead = new StreamReader(streamResponse);
                Char[] readBuff = new Char[256];
                int count = streamRead.Read(readBuff, 0, 256);



                while (count > 0)
                {
                    String outputData = new string(readBuff, 0, count);
                    json += outputData;
                    count = streamRead.Read(readBuff, 0, 256);
                }

                streamRead.DiscardBufferedData();
                streamResponse.Flush();
            }
            catch (Exception ex)
            {
                json = "";
            }

            return json;
        }

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

        public string QueryString(IDictionary<string, object> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(item.Key + "=" + item.Value);
            }
            return $"?{string.Join("&", list)}";
        }


    }
}
