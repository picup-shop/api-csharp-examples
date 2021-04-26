using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

namespace matting_api_request.httpUtils
{
    class HttpClientUtil
    {

        /* 
         * post请求返回字符串(同步)
         */
        public static string PostRequest(string uri, Dictionary<string, object> parameters, Dictionary<string, string> headers)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(parameters);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    //等待执行结果
                    var response = client.PostAsync(uri, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //等待结果
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

        /**
         * Psot图片或文件请求,返回二进制流
         *  
         */
        public static byte[] PostRequestReturnsByte(string uri, MultipartFormDataContent multipart, Dictionary<string, string> headers)
        {
            byte[] bytes = null;
            using (var client = new HttpClient())
            {
                try
                {
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    var response = client.PostAsync(uri, multipart).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //返回响应头格式为image/png代表请求成功
                        if (response.Content.Headers.ContentType.MediaType.Equals("image/png"))
                        {
                            bytes = response.Content.ReadAsByteArrayAsync().Result;
                        }
                        else
                        {
                            //图片处理失败返回JSON错误信息
                            string result = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine("图片处理失败: " + result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return bytes;
        }

        /**
         * Psot图片或文件请求,返回字符
         *  
         */
        public static string PostRequestReturnsStr(string uri, MultipartFormDataContent multipart, Dictionary<string, string> headers)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                try
                {
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    var response = client.PostAsync(uri, multipart).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        /**
         * Get请求
         */
        public static string GetRequest(string uri, Dictionary<string, string> parameters, Dictionary<string, string> headers)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var builder = new UriBuilder(uri);
                    //请求参数
                    if (parameters != null)
                    {
                        var query = new FormDataCollection(parameters).ReadAsNameValueCollection().ToString();
                        builder.Query = query;
                    }
                    //请求头
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    string url = builder.ToString();
                    Console.WriteLine("请求地址:" + url + "\n");
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}
