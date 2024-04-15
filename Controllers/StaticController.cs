using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.otoparcabul.com.Controllers
{
    public class StaticController : Controller
    {
        public ActionResult SiteHaritasi()
        {
            ViewData["Title"] = "Oto Parça Bul - Site Haritası";

            return View();
        }

        public ActionResult KullanimKosullari()
        {
            ViewData["Title"] = "Oto Parça Bul - Kullanım Koşulları";

            return View();
        }

        public ActionResult GizlilikPolitikasi()
        {
            ViewData["Title"] = "Oto Parça Bul - Gizlilik Politikası";

            return View();
        }
    }
}