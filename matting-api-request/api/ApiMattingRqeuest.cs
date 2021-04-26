using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace matting_api_request.api
{
    class ApiMattingRqeuest
    {
        //账号的API密钥
        private readonly string ApiKey;

        public ApiMattingRqeuest(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        /**
         * 抠图返回二进制文件流
         * imagePath 图片路径
         * crop 是否裁剪
         * bgColor 背景颜色
         * outPutPath 输出路径
         */
        public void MattingReturnsByteRequest(string uri, string imageFilePath, string imageFileName, string crop, string bgColor, string outPutPath)
        {
            Console.WriteLine("请求地址:" + uri + "\n");
            try
            {
                //请求头
                var heder = GetHeader();
                //图片上传
                var multipart = new MultipartFormDataContent()
                {
                    {new ByteArrayContent(File.ReadAllBytes(imageFilePath)), "file", imageFileName},
                    //裁剪
                    {new StringContent(crop), "crop"},
                    //图片背景
                    {new StringContent(bgColor), "bgcolor"}
                };
                byte[] bytes = HttpClientUtil.PostRequestReturnsByte(uri, multipart, heder);
                if (bytes != null)
                {
                    Console.WriteLine("success-----------\n");
                    File.WriteAllBytes(outPutPath, bytes);
                    Console.WriteLine("输出路径:" + outPutPath);
                    Console.WriteLine("----------------------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("抠图失败,错误信息:" + ex.Message);
            }

        }


        /**
         * 抠图返回base64字符串
         * imagePath 图片路径
         * crop 是否裁剪
         * bgColor 背景颜色
         * outPutPath 输出路径
         */
        public void MattingReturnsBase64Request(string uri, string imageFilePath, string imageFileName, string crop, string bgColor, string outPutPath)
        {
            Console.WriteLine("请求地址:" + uri + "\n");
            try
            {
                //请求头
                var heder = GetHeader();
                //图片上传
                var multipart = new MultipartFormDataContent()
                {
                    {new ByteArrayContent(File.ReadAllBytes(imageFilePath)), "file", imageFileName},
                    //裁剪
                    {new StringContent(crop), "crop"},
                    //图片背景
                    {new StringContent(bgColor), "bgcolor"}
                };
                string result = HttpClientUtil.PostRequestReturnsStr(uri, multipart, heder);
                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    string code = resultData["code"].ToString();
                    if (code.Equals("0"))
                    {
                        Console.WriteLine("success----------\n");
                        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultData["data"].ToString());
                        string imageBase64 = data["imageBase64"].ToString();
                        byte[] bytes = Convert.FromBase64String(imageBase64);
                        File.WriteAllBytes(outPutPath, bytes);
                        Console.WriteLine("输出路径:" + outPutPath);
                        Console.WriteLine("----------------------------------\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("抠图失败,错误信息:" + ex.Message);
            }

        }

        /**
         * 通过图片Url返回Base64结果
         */
        public void MattingByUrlRequest(string uri, string mattingType, string imageUrl, string crop, string bgColor, string outPutPath)
        {
            try
            {
                //请求头
                var header = GetHeader();
                //请求参数
                var parameters = new Dictionary<string, string>
                {
                    //抠图类型
                    {"mattingType", mattingType},
                    //裁剪
                    {"crop", crop},
                    //背景颜色
                    {"bgcolor", bgColor},
                    //图片URL
                    {"url", imageUrl}
                };
                string result = HttpClientUtil.GetRequest(uri, parameters, header);
                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    string code = resultData["code"].ToString();
                    if (code.Equals("0"))
                    {
                        Console.WriteLine("success----------\n");
                        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultData["data"].ToString());
                        string imageBase64 = data["imageBase64"].ToString();
                        byte[] bytes = Convert.FromBase64String(imageBase64);
                        File.WriteAllBytes(outPutPath, bytes);
                        Console.WriteLine("输出路径:" + outPutPath);
                        Console.WriteLine("----------------------------------\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("抠图失败,错误信息:" + ex.Message);
            }

        }

        /**
         * 请求头信息
         */
        private Dictionary<string, string> GetHeader()
        {
            var header = new Dictionary<string, string>
            {
                {"APIKEY", ApiKey}
            };
            return header;
        }
    }
}
