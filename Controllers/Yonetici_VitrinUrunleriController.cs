using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class Yonetici_VitrinUrunleriController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Index(string Tarih)
        {
            DateTime VitrinUrunleriTarih = new DateTime(Convert.ToInt32(Tarih.Substring(0, 4)), Convert.ToInt32(Tarih.Substring(5, 2)), Convert.ToInt32(Tarih.Substring(8, 2)));

            if (Request["Delete"] != null)
            {
                var SilinecekID = Convert.ToInt32(Request["Delete"]);
                var Silinecek = dc.ShowRooms.FirstOrDefault(d => d.ID == SilinecekID);

                if (Silinecek != null)
                {
                    dc.ShowRooms.Remove(Silinecek);
                    dc.SaveChanges();
                }
            }

            ViewData["VitrinUrunleriTarih"] = VitrinUrunleriTarih.ToString("yyyy-MM-dd");
            ViewData["Tarih"] = VitrinUrunleriTarih.ToShortDateString();
            ViewData["VitrinUrunleri"] = dc.ShowRooms.Where(d => d.Date == VitrinUrunleriTarih).ToList();

            return View();
        }

        public ActionResult UrunEkle()
        {
            int ProductID = Convert.ToInt32(Request["ProductID"]);
            string Date = Request["Date"];

            DateTime VitrinUrunleriTarih = new DateTime(Convert.ToInt32(Date.Substring(0, 4)), Convert.ToInt32(Date.Substring(5, 2)), Convert.ToInt32(Date.Substring(8, 2)));

            dc.ShowRooms.Add(new ShowRoom { Date = VitrinUrunleriTarih, ProductID = ProductID });

            dc.SaveChanges();

            ViewData["VitrinUrunleriTarih"] = VitrinUrunleriTarih.ToString("yyyy-MM-dd");
            ViewData["Tarih"] = VitrinUrunleriTarih.ToShortDateString();
            ViewData["VitrinUrunleri"] = dc.ShowRooms.Where(d => d.Date == VitrinUrunleriTarih).ToList();

            return Redirect("/yonetici/vitrin-urunleri/" + VitrinUrunleriTarih.ToString("yyyy-MM-dd"));
        }
    }
}