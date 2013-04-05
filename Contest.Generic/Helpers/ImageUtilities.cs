using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Mvc;
using System.Web.Routing;
using System.Drawing.Drawing2D;

namespace Contest.Generic.Helpers
{
    public static class ImageUtilities
    {
    //    public static byte[] ResizeToThumbnail(byte[] data, int maxSide)
    //    {
    //        byte[] result;

    //        using (MemoryStream startMemoryStream = new MemoryStream(),
    //                            newMemoryStream = new MemoryStream())
    //        {
    //            startMemoryStream.Write(data, 0, data.Length);

    //            Bitmap startBitmap = new Bitmap(startMemoryStream);

    //            int newHeight;
    //            int newWidth;
    //            double HW_ratio;
    //            if (startBitmap.Height > startBitmap.Width)
    //            {
    //                newHeight = maxSide;
    //                HW_ratio = (double)((double)maxSide / (double)startBitmap.Height);
    //                newWidth = (int)(HW_ratio * (double)startBitmap.Width);
    //            }
    //            else
    //            {
    //                newWidth = maxSide;
    //                HW_ratio = (double)((double)maxSide / (double)startBitmap.Width);
    //                newHeight = (int)(HW_ratio * (double)startBitmap.Height);
    //            }

    //            Bitmap newBitmap = new Bitmap(newWidth, newHeight);
 
    //            newBitmap = ResizeImage(startBitmap, newWidth, newHeight);
                
    //            newBitmap.Save(newMemoryStream,ImageFormat.Png);
 
    //            result = newMemoryStream.ToArray();
    //        }

    //        return result;
    //    }

        public static IHtmlString ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues, object htmlAttributes, object imgHtmlAttributes)
        {
            UrlHelper urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            TagBuilder imgTag = new TagBuilder("img");
            imgTag.MergeAttribute("src", imgSrc);
            imgTag.MergeAttributes((IDictionary<string, string>)imgHtmlAttributes, true);
            string url = urlHelper.Action(actionName, controllerName, routeValues);

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgTag.ToString();
            imglink.MergeAttributes((IDictionary<string, string>)htmlAttributes, true);

            return new MvcHtmlString(imglink.ToString());
        }

        public static void SaveImage(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            //int sourceWidth = imgToResize.Width;
            //int sourceHeight = imgToResize.Height;

            //float nPercent = 0;
            //float nPercentW = 0;
            //float nPercentH = 0;

            //nPercentW = ((float)size.Width / (float)sourceWidth);
            //nPercentH = ((float)size.Height / (float)sourceHeight);

            //if (nPercentH < nPercentW)
            //    nPercent = nPercentH;
            //else
            //    nPercent = nPercentW;

            //int destWidth = (int)(sourceWidth * nPercent);
            //int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
            g.Dispose();

            return (Image)b;
        }
    }
}