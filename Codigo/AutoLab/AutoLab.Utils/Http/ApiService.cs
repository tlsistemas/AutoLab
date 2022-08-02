using Newtonsoft.Json;
using AutoLab.Utils.Http.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using AutoLab.Utils.Http.Interface;

namespace AutoLab.Utils.Http
{
    public class ApiService : IApiService
    {
        public const string ContentType = "application/json";
        const long MaxBufferSize = 9999999;

        public String GetWebRequest(String url)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.Timeout = 300000;

                var resp = req.GetResponse();
                var outStream = resp.GetResponseStream();
                string output = "";
                using (StreamReader rdr = new StreamReader(outStream))
                {
                    output = rdr.ReadToEnd();
                }


                return output;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<BaseResponse> Get(string url, string tokenHash = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Timeout = 300000;
                if (tokenHash != null)
                {
                    request.Headers.Add("Authorization", "Bearer " + tokenHash);
                }

                string html = string.Empty;
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                var BaseResponseApi = JsonConvert.DeserializeObject<BaseResponse>(html);
                return BaseResponseApi;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError(ex.GetType().Name, ex.Message);
            }

        }

        public async Task<BaseResponse<T>> Get<T>(string url, string tokenHash = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Timeout = 300000;
                if (tokenHash != null)
                {
                    request.Headers.Add("Authorization", "Bearer " + tokenHash);
                }

                string html = string.Empty;
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                var BaseResponseApi = JsonConvert.DeserializeObject<BaseResponse<T>>(html);
                return BaseResponseApi;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError<T>(ex.GetType().Name, ex.Message);
            }

        }

        public async Task<BaseResponse> Delete(string url, string tokenHash = null)
        {
            try
            {
                using (var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(2)
                })
                {
                    if (tokenHash != null)
                    {
                        httpClient.DefaultRequestHeaders.TryGetValues("Authorization", out IEnumerable<string> list);

                        if (!ReferenceEquals(list, null))
                            httpClient.DefaultRequestHeaders.Remove("Authorization");

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenHash}");
                    }

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
                    var request = await httpClient.DeleteAsync($"{url}");
                    var BaseResponse = await request.Content.ReadAsStringAsync();

                    if (!request.IsSuccessStatusCode) return MsgError(BaseResponse);

                    return JsonConvert.DeserializeObject<BaseResponse>(BaseResponse);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError(ex.GetType().Name, ex.Message);
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

        public async Task<BaseResponse> Post(string url, string json, string tokenHash = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()
                {
                    MaxResponseContentBufferSize = MaxBufferSize,
                    Timeout = TimeSpan.FromMinutes(5) 
                })
                {
                    if (tokenHash != null)
                    {
                        httpClient.DefaultRequestHeaders.TryGetValues("Authorization", out IEnumerable<string> list);

                        if (!ReferenceEquals(list, null))
                            httpClient.DefaultRequestHeaders.Remove("Authorization");

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenHash}");
                    }

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

                    HttpResponseMessage request;
                    if (!string.IsNullOrEmpty(json))
                    {
                        var content = new StringContent(json, Encoding.UTF8, ContentType);
                        request = await httpClient.PostAsync($"{url}", content);
                    }
                    else
                    {
                        request = await httpClient.PostAsync($"{url}", null);
                    }
                    var BaseResponse = await request.Content.ReadAsStringAsync();

                    if (request.IsSuccessStatusCode)
                    {
                        var BaseResponseApi = JsonConvert.DeserializeObject<BaseResponse>(BaseResponse);
                        if (BaseResponseApi.Data == null)
                        {
                            BaseResponseApi.Data = BaseResponse;
                        }
                        return BaseResponseApi;
                    }
                    else
                    {
                        return MsgError(BaseResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError(ex.GetType().Name, ex.Message);
            }
        }
        public async Task<BaseResponse> Put(string url, string json, string tokenHash = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()
                {
                    MaxResponseContentBufferSize = MaxBufferSize,
                    Timeout = TimeSpan.FromMinutes(5)
                })
                {
                    if (tokenHash != null)
                    {
                        httpClient.DefaultRequestHeaders.TryGetValues("Authorization", out IEnumerable<string> list);

                        if (!ReferenceEquals(list, null))
                            httpClient.DefaultRequestHeaders.Remove("Authorization");

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenHash}");
                    }

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

                    HttpResponseMessage request;
                    if (!string.IsNullOrEmpty(json))
                    {
                        var content = new StringContent(json, Encoding.UTF8, ContentType);
                        request = await httpClient.PutAsync($"{url}", content);
                    }
                    else
                    {
                        request = await httpClient.PutAsync($"{url}", null);
                    }
                    var BaseResponse = await request.Content.ReadAsStringAsync();

                    if (request.IsSuccessStatusCode)
                    {
                        var BaseResponseApi = JsonConvert.DeserializeObject<BaseResponse>(BaseResponse);
                        return BaseResponseApi;
                    }
                    else
                    {
                        return MsgError(BaseResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError(ex.GetType().Name, ex.Message);
            }
        }

        public async Task<BaseResponse> Update(string url, string json, string tokenHash = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()
                {
                    MaxResponseContentBufferSize = MaxBufferSize,
                    Timeout = TimeSpan.FromMinutes(5)
                })
                {
                    if (tokenHash != null)
                    {
                        httpClient.DefaultRequestHeaders.TryGetValues("Authorization", out IEnumerable<string> list);

                        if (!ReferenceEquals(list, null))
                            httpClient.DefaultRequestHeaders.Remove("Authorization");

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenHash}");
                    }


                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

                    var content = new StringContent(json, Encoding.UTF8, ContentType);
                    var request = await httpClient.PutAsync($"{url}", content);
                    var BaseResponse = await request.Content.ReadAsStringAsync();

                    if (request.IsSuccessStatusCode)
                    {
                        var BaseResponseApi = JsonConvert.DeserializeObject<BaseResponse>(BaseResponse);
                        return BaseResponseApi;
                    }
                    else
                    {
                        return MsgError(BaseResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError(ex.GetType().Name, ex.Message);
            }
        }

        public async Task<BaseResponse> Patch(string url, string json, string tokenHash = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()
                {
                    MaxResponseContentBufferSize = MaxBufferSize,
                    Timeout = TimeSpan.FromMinutes(5)
                })
                {
                    if (tokenHash != null)
                    {
                        httpClient.DefaultRequestHeaders.TryGetValues("Authorization", out IEnumerable<string> list);

                        if (!ReferenceEquals(list, null))
                            httpClient.DefaultRequestHeaders.Remove("Authorization");

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenHash}");
                    }


                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{url}")
                    {
                        Content = new StringContent(json, Encoding.UTF8, ContentType)
                    };

                    var BaseResponse = await httpClient.SendAsync(request);
                    var BaseResponseString = await BaseResponse.Content.ReadAsStringAsync();


                    if (BaseResponse.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<BaseResponse>(BaseResponseString);

                    return new BaseResponse { Success = false };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return MsgError(ex.GetType().Name, ex.Message);
            }
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
