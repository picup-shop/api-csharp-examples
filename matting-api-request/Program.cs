using matting_api_request.api;
using System;
using System.IO;
namespace matting_api_request
{
    class Program
    {
        //账号密钥
        private const string APIKEY = "b3657935cf564118b66db1feb8a122f0";
        //请求地址
        private const string REQUEST_URL = "http://www.picup.shop/api/v1";

        //读取路径(此路径为当前项目下的图片文件夹路径 )
        private static readonly string READ_IMAGE_PATH;

        //抠图(包括人像,物体,通用,头像,美化,动漫化)
        private static readonly ApiMattingRqeuest apiMattingRqeuest;

        //图片URL
        private static readonly string ImageUrl;
        private static readonly string ImageUrl2;
        private static readonly string ImageUrl3;

        static Program()
        {
            READ_IMAGE_PATH = Path.GetFullPath("../../..") + @"\images\";
            apiMattingRqeuest = new ApiMattingRqeuest(APIKEY);
            ImageUrl = "https://pics6.baidu.com/feed/574e9258d109b3de9ff792ddf1564f87810a4c2d.jpeg?token=4a333e5c57d8a35cbe23682083316697&s=58898F5566027355008448A80300E00A";
            ImageUrl2 = "http://images.news18.com/ibnlive/uploads/2018/09/Ducati-Panigale-959-Corsa.jpg";
            ImageUrl3 = "http://wdpicup.oss-cn-hangzhou.aliyuncs.com/matting_original/2021/04/25/f1d95be6da2e490588e55e0757ac34eb.jpg?Expires=1619936560&OSSAccessKeyId=LTAIzt3dzL2GfSyG&Signature=kgXZ00EDzaTipKf8ABGMc%2BMGA%2FI%3D";
        }

        static void Main(string[] args)
        {
            //IdPhoto();
            //MattingPortrait();
            //MattingBody();
            //MattingUniversal();
            //MattingAvatar();
            //MattingBeautify();
            //ImageFix();
            //MattingAnime();
            //StyleTransfer();
            Console.ReadKey();
        }


        //证件照API请求
        private static void IdPhoto()
        {
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath() + @"\id_photo.txt";
            string IdPhotoRqeuestUri = REQUEST_URL + "/idphoto/printLayout";
            var mattingRequest = new ApiIdPhotoRequest(APIKEY, IdPhotoRqeuestUri);
            mattingRequest.IdPhoto(READ_IMAGE_PATH + "boy.png", outPutPath);
        }

        //人像抠图（mattingType=1）
        private static void MattingPortrait()
        {
            //裁剪
            string crop = "";
            //背景色
            string bgcolor = "";
            //图片路径
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //图片名称
            string imageFileName = Path.GetFileName(imagePath);
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath();
            //返回二进制文件流
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=1", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\portrait_byte.png");
            //返回base64字符串
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=1", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\portrait_base64.png");
            //通过图片URL返回base64字符串
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "1", ImageUrl, crop, bgcolor, outPutPath + @"\portrait_url.png");
        }

        //物体抠图（mattingType=2）
        private static void MattingBody()
        {
            //裁剪
            string crop = "";
            //背景色
            string bgcolor = "";
            //图片路径
            string imagePath = READ_IMAGE_PATH + "ducati.jpg";
            //图片名称
            string imageFileName = Path.GetFileName(imagePath);
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath();
            //返回二进制文件流
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=2", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\body_byte.png");
            //返回base64字符串
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=2", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\body_base64.png");
            //通过图片URL返回base64字符串
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "2", ImageUrl2, crop, bgcolor, outPutPath + @"\body_url.png");
        }

        //通用抠图（mattingType=6）
        private static void MattingUniversal()
        {
            //裁剪
            string crop = "";
            //背景色
            string bgcolor = "";
            //图片路径
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //图片名称
            string imageFileName = Path.GetFileName(imagePath);
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath();
            //返回二进制文件流
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=6", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\universal_byte.png");
            //返回base64字符串
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=6", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\universal_base64.png");
            //通过图片URL返回base64字符串
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "6", ImageUrl, crop, bgcolor, outPutPath + @"\universal_url.png");
        }

        //头像抠图（mattingType=3）
        private static void MattingAvatar()
        {
            //裁剪
            string crop = "";
            //背景色
            string bgcolor = "";
            //图片路径
            string imagePath = READ_IMAGE_PATH + "child.jpg";
            //图片名称
            string imageFileName = Path.GetFileName(imagePath);
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath();
            //返回二进制文件流
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=3", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\avatar_byte.png");
            //返回base64字符串
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=3", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\avatar_base64.png");
            //通过图片URL返回base64字符串
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "3", ImageUrl3, crop, bgcolor, outPutPath + @"\avatar_url.png");
        }

        //美化
        private static void MattingBeautify()
        {
            //图片路径
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //图片名称
            string imageFileName = Path.GetFileName(imagePath);
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath();
            //返回二进制文件流
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=4", imagePath, imageFileName, "", "", outPutPath + @"\beautify_byte.png");
            //返回base64字符串
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=4", imagePath, imageFileName, "", "", outPutPath + @"\beautify_base64.png");
        }

        //图像修复
        private static void ImageFix()
        {
            //修复图
            string fix_image = READ_IMAGE_PATH + "image_fix.jpg";
            //mask图
            string mask_image = READ_IMAGE_PATH + "mask.jpg";
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath() + @"\image_fix.txt";
            var apiImageFixRequest = new ApiImageFixRequest(APIKEY, REQUEST_URL + "/imageFix");
            apiImageFixRequest.ImageFixRequest(fix_image, mask_image, outPutPath);
        }

        //动漫化
        private static void MattingAnime()
        {
            //图片路径
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //图片名称
            string imageFileName = Path.GetFileName(imagePath);
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath();
            //返回二进制文件流
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=11", imagePath, imageFileName, "", "", outPutPath + @"\anime_byte.png");
            //返回base64字符串
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=11", imagePath, imageFileName, "", "", outPutPath + @"\anime_base64.png");
            //通过图片URL返回base64字符串
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "11", ImageUrl, null, null, outPutPath + @"\anime_url.png");
        }

        //风格迁移
        private static void StyleTransfer()
        {
            //图片路径
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //风格图片路径
            string styleImagePath = READ_IMAGE_PATH + "style.jpg";
            //输出路径（方便展示结果,可以自定义输出路径）
            string outPutPath = GetOutPutPath() + @"\style_transfer.txt";
            var apiStyleTransfer = new ApiStyleTransferRequest(APIKEY, REQUEST_URL + "/styleTransferBase64");
            apiStyleTransfer.StyleTranferRequest(imagePath, styleImagePath, outPutPath);
        }

        //输出路径(仅为了方便展示结果)
        private static string GetOutPutPath()
        {
            string outPutPath = Environment.CurrentDirectory + @"\result";
            if (!Directory.Exists(outPutPath))
            {
                Directory.CreateDirectory(outPutPath);
            }
            return outPutPath;
        }
    }
}
