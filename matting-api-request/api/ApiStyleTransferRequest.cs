using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace matting_api_request.api
{
    class ApiStyleTransferRequest
    {
        //账号的密钥
        private readonly string ApiKey;

        //请求地址
        private readonly string RqeuestUrl;

        public ApiStyleTransferRequest(string apiKey, string requestUrl)
        {
            this.ApiKey = apiKey;
            this.RqeuestUrl = requestUrl;
        }

        public void StyleTranferRequest(string imagePath, string styleImagePath, string outPutPath)
        {
            Console.WriteLine("请求地址:" + RqeuestUrl + "\n");
            try
            {
                //图片Base64
                byte[] bytes = File.ReadAllBytes(imagePath);
                string contentBase64 = Convert.ToBase64String(bytes);
                //风格Base64
                byte[] styleByte = File.ReadAllBytes(styleImagePath);
                string styleBase64 = Convert.ToBase64String(styleByte);

                //请求头
                var header = new Dictionary<string, string>
                {
                    {"APIKEY", ApiKey}
                };

                //请求参数
                var parameters = new Dictionary<string, object>
                {
                    {"contentBase64", contentBase64},
                    {"styleBase64", styleBase64}
                };
                string result = HttpClientUtil.PostRequest(RqeuestUrl, parameters, header);
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
                Console.WriteLine("风格迁移API请求失败:" + ex.Message);
            }
        }
    }
}
