using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace matting_api_request.api
{
    class ApiIdPhotoRequest
    {
        //账号的密钥
        private readonly string ApiKey;

        //请求地址
        private readonly string RqeustUri;


        public ApiIdPhotoRequest(string apikey, string uri)
        {
            this.ApiKey = apikey;
            this.RqeustUri = uri;
        }

        //证件照API
        //imagePath: 图片路径
        //outPutPath: 输出图片路径
        public void IdPhoto(string imagePath, string outPutPath)
        {
            Console.WriteLine("请求地址:" + RqeustUri + "\n");
            try
            {
                //请求图片的base64
                byte[] byes = File.ReadAllBytes(imagePath);
                string imageBase64 = Convert.ToBase64String(byes);

                //请求头
                var header = new Dictionary<string, string> {
                    {"APIKEY", ApiKey}
                };

                //请求体
                var parameters = new Dictionary<string, object>
                {
                    //图片Base64
                    {"base64", imageBase64},
                    //背景颜色
                    {"bgColor", "FFFFFF"},
                    //证件照打印DPI
                    {"dpi", 300},
                    //证件照物理高度
                    {"mmHeight", 35},
                    //证件照物理宽度
                    {"mmWidth", 25},
                    //排版背景色
                    {"printBgColor", "FFFFFF"},
                     //打印的排版尺寸高度，单位为毫米
                    {"printMmHeight", 210},
                    //打印的排版尺寸宽度，单位为毫米
                    {"printMmWidth", 150},
                    //换装参数,填入此参数额外扣除一个点数
                    //{"dress", ""}
                };
                string result = HttpClientUtil.PostRequest(RqeustUri, parameters, header);
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
            catch (Exception e)
            {
                Console.WriteLine("证件照API请求错误,错误信息:" + e.Message);
            }
        }
    }
}
