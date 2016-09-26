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
using System.Net;

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

        public ActionResult Result(string AppId, string FBUserId, string username)
        {
            ViewBag.resulturl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~") + "//FBUserInformation//" + AppId + "//" + FBUserId + "//" + username + ".jpg"); // /TESTERS/Default6.aspx + "output.jpg";

            return View();
        }

        [HttpPost]
        public ActionResult ProcessAppLogin(string AppId, string FBUserId, string profileurl, string username)
        {
            Image imgbackground = Image.FromFile(Server.MapPath("~/Images") + "//Happy-Navratri-Photos.jpg");
            Graphics g = Graphics.FromImage(imgbackground);

            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(profileurl);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        g.DrawImage(yourImage, new Point(20, 20));
                    }
                }

            }
            Bitmap bitmap_Background = (Bitmap)imgbackground;

            var UserProfileData = Server.MapPath("~/FBUserInformation/" + AppId + "//" + FBUserId);
            if (!Directory.Exists(UserProfileData))
            {
                Directory.CreateDirectory(UserProfileData);
            }
            imgbackground.Save(UserProfileData + "\\" + username + ".jpg", ImageFormat.Jpeg);
            imgbackground.Dispose();
            return Json("done", JsonRequestBehavior.AllowGet);
        }
    }
}
