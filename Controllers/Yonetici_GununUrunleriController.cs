using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class Yonetici_GununUrunleriController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Index(string Tarih)
        {
            DateTime GununUrunleriTarih = new DateTime(Convert.ToInt32(Tarih.Substring(0, 4)), Convert.ToInt32(Tarih.Substring(5, 2)), Convert.ToInt32(Tarih.Substring(8, 2)));

            if (Request["Delete"] != null)
            {
                var SilinecekID = Convert.ToInt32(Request["Delete"]);
                var Silinecek = dc.DailyChances.FirstOrDefault(d => d.ID == SilinecekID);

                if (Silinecek != null)
                {
                    dc.DailyChances.Remove(Silinecek);
                    dc.SaveChanges();
                }
            }

            ViewData["GununUrunleriTarih"] = GununUrunleriTarih.ToString("yyyy-MM-dd");
            ViewData["Tarih"] = GununUrunleriTarih.ToShortDateString();
            ViewData["GununUrunleri"] = dc.DailyChances.Where(d => d.Date == GununUrunleriTarih).ToList();

            return View();
        }

        public ActionResult UrunEkle()
        {
            int ProductID = Convert.ToInt32(Request["ProductID"]);
            string Date = Request["Date"];

            DateTime GununUrunleriTarih = new DateTime(Convert.ToInt32(Date.Substring(0, 4)), Convert.ToInt32(Date.Substring(5, 2)), Convert.ToInt32(Date.Substring(8, 2)));

            dc.DailyChances.Add(new DailyChance { Date = GununUrunleriTarih, ProductID = ProductID });

            dc.SaveChanges();

            ViewData["GununUrunleriTarih"] = GununUrunleriTarih.ToString("yyyy-MM-dd");
            ViewData["Tarih"] = GununUrunleriTarih.ToShortDateString();
            ViewData["GununUrunleri"] = dc.DailyChances.Where(d => d.Date == GununUrunleriTarih).ToList();

            return Redirect("/yonetici/gunun-urunleri/" + GununUrunleriTarih.ToString("yyyy-MM-dd"));
        }
    }
}