using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Facebook;
using Microsoft.AspNet.Facebook.Client;
using SampleFacebookBirthdayApp.Models;
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace SampleFacebookBirthdayApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string context)
        {
            return View();
        }
        [FacebookAuthorize()]
        public ActionResult MyFBApp(FacebookContext context)
        {
            return View();
        }
        private static Stream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);

            return new MemoryStream(imageData);
        }
       
        public ActionResult Result(FacebookContext context)
        {
            
            Image imgbackground = Image.FromFile(Server.MapPath("~/Images") + "//background.png");
            Graphics g = Graphics.FromImage(imgbackground);


            g.DrawImage(Image.FromStream(GetStreamFromUrl("https://cdn0.iconfinder.com/data/icons/social-messaging-ui-color-shapes/128/user-male-circle-blue-128.png")), new Point(50, 50));

           // g.DrawImage(Image.FromStream(GetStreamFromUrl("http://image.flaticon.com/icons/png/128/149/149071.png")), new Point(150, 150));

            Bitmap bitmap_Background = (Bitmap)imgbackground;
            imgbackground.Save(Server.MapPath("~/Images") + "\\output.jpg", ImageFormat.Jpeg);
            imgbackground.Dispose();
            ViewBag.resulturl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) +"//Images//output.jpg"; // /TESTERS/Default6.aspx + "output.jpg";
            return View();
            //MemoryStream ms = new MemoryStream();
            //imgbackground.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //return File(ms.ToArray(), "image/png");
        }
    }
}
