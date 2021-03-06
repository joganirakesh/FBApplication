﻿using System.Collections.Generic;
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

        [HandleError]
        public ActionResult Index(string context)
        {

            return View();
        }

        [FacebookAuthorize()]
        public ActionResult FacebookAuth(FacebookContext context)
        {
            return View();
        }

        public ActionResult NavratriSpecial()
        {
            ViewBag.AppName = "NavratriSpecial";
            ViewBag.AppTitle = "ન​વરાત્રી! એ હાલો હાલો! ગરબે રમવા";
            ViewBag.AppDescription = "ન​વરાત્રી! એ હાલો હાલો! ગરબે રમવા";

            return View();
        }

        public ActionResult WishNavratri()
        {
            ViewBag.AppName = "WishNavratri";
            ViewBag.AppTitle = "WISH HAPPY NAVRATRI TO YOUR FRIENDS !";
            ViewBag.AppDescription = "Wish happy navratri to your friends !";
            return View();
        }
        public ActionResult HappyGandhiJayanti()
        {
            ViewBag.AppName = "HappyGandhiJayanti";
            ViewBag.AppTitle = "WISH HAPPY BIRTHDAY TO MAHATMA GANDHIJI";
            ViewBag.AppDescription = "My life is my message.' - Happy Gandhi Jayanti";
            return View();
        }
        public ActionResult HappyDasara()
        {
            ViewBag.AppName = "HappyDasara";
            ViewBag.AppTitle = "This special occassion may fulfill all Ur dreams came true.Happy Dasara";
            ViewBag.AppDescription = "This special occassion may fulfill all Ur dreams came true.Happy Dasara";
            return View();
        }

        public ActionResult HappyDiwali2016()
        {
            ViewBag.AppName = "HappyDiwali2016";
            ViewBag.AppTitle = "May thousands of lamps beam up your life. Happy Diwali!";
            ViewBag.AppDescription = "May thousands of lamps beam up your life. Happy Diwali!";
            return View();
        }

        public ActionResult BhaiDoojWish2016()
        {
            ViewBag.AppName = "BhaiDoojWish2016";
            ViewBag.AppTitle = "May this auspicious occasion bring you all the prosperity and good luck in your way of life. Happy Bhai Beej.";
            ViewBag.AppDescription = "May this auspicious occasion bring you all the prosperity and good luck in your way of life. Happy Bhai Beej.";
            return View();
        }

        public ActionResult Result(string AppName, string FBUserId, string username)
        {
            ViewBag.AppName = AppName;
            ViewBag.AppTitle = "ન​વરાત્રી! એ હાલો હાલો! ગરબે રમવા";
            ViewBag.resulturl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~") + "//FBUserInformation//" + AppName + "//" + FBUserId + "//" + username + ".jpg"); // /TESTERS/Default6.aspx + "output.jpg";

            return View();
        }

        private void DrawBitmapWithBorder(Bitmap bmp, Point pos, Graphics g)
        {
            const int borderSize = 20;

            using (Brush border = new SolidBrush(Color.White /* Change it to whichever color you want. */))
            {
                g.FillRectangle(border, pos.X - borderSize, pos.Y - borderSize,
                    bmp.Width + borderSize, bmp.Height + borderSize);
            }

            g.DrawImage(bmp, pos);
        }
        public void BorderImage()
        {
            string username = "rakeshjogani";
            string profileurl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTNryR26xQpEIOduxYUBfywVxdz31P0i2QZM_zvh-TO5RATZ8ydcw";
            string AppName = "111";
            string FBUserId = "111";
            Image imgbackground = Image.FromFile(Server.MapPath("~/Images") + "//background.png");
            Graphics g = Graphics.FromImage(imgbackground);

            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(profileurl);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    //var yourImage = Image.FromStream(mem);
                    var yourImage = Image.FromFile(Server.MapPath("~/Images") + "//userprofile.png");
                    yourImage = ImageHelper.RoundCorners(yourImage, ((yourImage.Width) / 2), Color.Gray, 0);

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
        }

        #endregion

        #region WebMethod

        [HttpPost]
        public ActionResult NavratriSpecial(string AppName, string FBUserId, string profileurl, string username)
        {
            Image imgbackground = Image.FromFile(Server.MapPath("~/Images") + "//Happy-Navratri-Photos.jpg");
            Graphics g = Graphics.FromImage(imgbackground);

            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(profileurl);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    var yourImage = Image.FromStream(mem);
                    yourImage = ImageHelper.RoundCorners(yourImage, ((yourImage.Width) / 2), Color.White, 10);

                    using (yourImage)
                    {
                        g.DrawImage(yourImage, new Point(30, 30));

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
