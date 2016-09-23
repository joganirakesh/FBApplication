using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Facebook;
using Microsoft.AspNet.Facebook.Client;
using SampleFacebookBirthdayApp.Models;
using System;

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
    }
}
