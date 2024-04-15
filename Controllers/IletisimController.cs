using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.otoparcabul.com.Controllers
{
    public class IletisimController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Oto Parça Bul - Bize Ulaşın";

            return View();
        }
    }
}