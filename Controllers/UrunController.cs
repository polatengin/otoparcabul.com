using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class UrunController : Controller
    {
        OtoParcaBulEntities e = new OtoParcaBulEntities();

        public ActionResult Index(string ProductCode, string UrlName)
        {
            Product Urun = e.Products.FirstOrDefault(p => p.ProductCode == ProductCode);

            ProductHit HitCounter = e.ProductHits.FirstOrDefault(ph => ph.ProductID == Urun.ID);

            if (HitCounter == null)
            {
                HitCounter = new ProductHit();
                HitCounter.ProductID = Urun.ID;

                e.ProductHits.Add(HitCounter);
            }

            HitCounter.HitCount += 1;
            e.SaveChanges();

            ViewData["Product"] = Urun;

            ViewData["Title"] = "Oto Parça Bul - Ürün Detay : " + Urun.Title;

            return View();
        }

        public ActionResult Ara()
        {
            ViewData["Title"] = "Oto Parça Bul - Ürün Ara";

            return View();
        }
    }
}