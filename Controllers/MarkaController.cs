using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class MarkaController : Controller
    {
        OtoParcaBulEntities e = new OtoParcaBulEntities();

        public ActionResult Liste()
        {
            ViewData["Title"] = "Oto Parça Bul - Marka Liste";

            return View();
        }

        public ActionResult Index(string UrlName)
        {
            Brand Marka = (HttpContext.Application["Brands"] as List<Brand>).FirstOrDefault(c => c.UrlName == UrlName);

            List<Product> Urunler = e.BrandModels.Where(bm => bm.BrandID == Marka.ID).SelectMany(b => b.Products).Where(p => p.InStock > 0 && !p.IsDeleted).OrderByDescending(p => p.LastModifiedDate).ToList();

            ViewData["Marka"] = Marka;
            ViewData["Urunler"] = Urunler;

            ViewData["Title"] = "Oto Parça Bul - Marka : " + Marka.Name;

            return View();
        }
    }
}