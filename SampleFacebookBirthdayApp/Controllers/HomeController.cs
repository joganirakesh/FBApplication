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
using SampleFacebookBirthdayApp.Helpers;

namespace SampleFacebookBirthdayApp.Controllers
{
    public class HomeController : Controller
    {
        #region Page Controller

        public ActionResult Index(string context)
        {
            return View();
        }

        [FacebookAuthorize()]
        public ActionResult FacebookAuth(FacebookContext context)
        {
            return View();
        }

        [FacebookAuthorize()]
        public ActionResult MyFBApp(FacebookContext context)
        {
            return View();
        }

        public ActionResult Navratri2016()
        {
            string profileurl = "http://arswiki.info/twiki/pub/Main/UserProfileHeader/default-user-profile.jpg";
            string AppName = "111";
            string FBUserId = "111";
            string username = "rakeshjogani";
            Image imgbackground = Image.FromFile(Server.MapPath("~/111/111") + "//Happy-Navratri-Photos.jpg");

            Graphics g = Graphics.FromImage(imgbackground);

            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(profileurl);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    Image yourImage = Image.FromStream(mem);
                    yourImage = ImageHelper.RoundCorners(yourImage, 80);
                    
                    using (yourImage)
                    {

                        g.DrawImage(yourImage, new Point(20, 20));
                    }
                }

            }
            Bitmap bitmap_Background = (Bitmap)imgbackground;

            var UserProfileData = Server.MapPath("~/FBUserInformation/" + AppName + "//" + FBUserId);
            if (!Directory.Exists(UserProfileData))
            {
                Directory.CreateDirectory(UserProfileData);
            }
            imgbackground.Save(UserProfileData + "\\" + username + ".jpg", ImageFormat.Jpeg);
            imgbackground.Dispose();


            ViewBag.AppName = "Navratri2016";
            //ViewBag.AppTitle = "ન​વરાત્રી! એ હાલો હાલો! ગરબે રમવા";
            ViewBag.AppTitle = "";
            return View();
        }

        public ActionResult WishNavratri()
        {
            ViewBag.AppName = "WishNavratri";
            ViewBag.AppTitle = "WISH HAPPY NAVRATRI TO YOUR FRIENDS !";
            return View();
        }


        private static Stream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);

            return new MemoryStream(imageData);
        }

        public ActionResult Result(string AppName, string FBUserId, string username)
        {
            ViewBag.AppName = AppName;
            ViewBag.AppTitle = "ન​વરાત્રી! એ હાલો હાલો! ગરબે રમવા";
            ViewBag.resulturl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~") + "//FBUserInformation//" + AppName + "//" + FBUserId + "//" + username + ".jpg"); // /TESTERS/Default6.aspx + "output.jpg";

            return View();
        }

        #endregion

        #region WebMethod

        [HttpPost]
        public ActionResult Navratri2016(string AppName, string FBUserId, string profileurl, string username)
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

            var UserProfileData = Server.MapPath("~/FBUserInformation/" + AppName + "//" + FBUserId);
            if (!Directory.Exists(UserProfileData))
            {
                Directory.CreateDirectory(UserProfileData);
            }
            imgbackground.Save(UserProfileData + "\\" + username + ".jpg", ImageFormat.Jpeg);
            imgbackground.Dispose();
            return Json("done", JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
