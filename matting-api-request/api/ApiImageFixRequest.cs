using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace matting_api_request.api
{
    class ApiImageFixRequest
    {
        //账号密钥
        private readonly string ApiKey;
        //请求地址
        private readonly string RequestUrl;

        public ApiImageFixRequest(string apiKey, string requestUrl)
        {
            this.ApiKey = apiKey;
            this.RequestUrl = requestUrl;
        }

        /**
         * 图片修复（示例使用maskBase64方式请求）
         * imagePath 图片路径
         * maskImagePath mask图片的base64 
         */
        public void ImageFixRequest(string imagePath, string maskImagePath, string outPutPath)
        {
            Console.WriteLine("请求地址:" + RequestUrl + "\n");
            try
            {
                //需要修复的图片
                byte[] bytes = File.ReadAllBytes(imagePath);
                string base64 = Convert.ToBase64String(bytes);
                //mask图片
                byte[] mask = File.ReadAllBytes(maskImagePath);
                string maskBase64 = Convert.ToBase64String(mask);

                //矩形区域数组参数（使用矩形区域就不需要mask图片）
                //var rectangles = new List<Dictionary<string, object>>
                //{
                //    {
                //        new Dictionary<string, object>{
                //            {"x", 160},
                //            {"y",250},
                //            {"width",200},
                //            {"height",200}
                //        }
                //    }
                //};

                //请求体
                var parameters = new Dictionary<string, object>
                {
                    //修复图片的base64
                    {"base64", base64},
                    //mask图片的base64
                    {"maskBase64", maskBase64}
                    //矩形区域请求参数
                    //{"rectangles", rectangles }
                };
                //请求头
                var header = new Dictionary<string, string>
                {
                        {"APIKEY", ApiKey}
                };
                string result = HttpClientUtil.PostRequest(RequestUrl, parameters, header);
                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    string code = resultData["code"].ToString();
                    if (code.Equals("0"))
                    {
                        Console.WriteLine("success----------\n");
                        File.AppendAllText(outPutPath, result);
                        Console.WriteLine("输出路径:" + outPutPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("图像修复API请求错误:" + ex.Message);
            }
        }
    }
}
