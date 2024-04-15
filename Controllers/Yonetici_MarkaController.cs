using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class Yonetici_MarkaController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Liste()
        {
            if (Request["Delete"] != null)
            {
                int MarkaID = Convert.ToInt32(Request["Delete"]);

                var SilinecekMarka = dc.Brands.First(b => b.ID == MarkaID);

                SilinecekMarka.IsActive = false;

                dc.SaveChanges();

                HttpContext.Application["Brands"] = dc.Brands.Where(e => e.IsActive).OrderBy(e => e.Name).ToList();
            }

            return View();
        }

        public ActionResult Kaydet(int VehicleTypeID, int BrandID, string Name)
        {
            Brand Marka = null;

            if (BrandID == 0)
            {
                Marka = new Brand();

                dc.Brands.Add(Marka);
            }
            else
            {
                Marka = dc.Brands.First(b => b.ID == BrandID);
            }

            var vehicleType = (HttpContext.Application["VehicleTypes"] as List<VehicleType>).FirstOrDefault(e => e.ID == VehicleTypeID && e.IsActive);

            Marka.VehicleTypeID = VehicleTypeID;
            Marka.Name = Name;
            Marka.UrlName = HtmlHelpers.ToUrlName(vehicleType.Name + "-" + Name);
            Marka.IsActive = true;

            dc.SaveChanges();

            HttpContext.Application["Brands"] = dc.Brands.Where(e => e.IsActive).OrderBy(e => e.Name).ToList();

            return RedirectToAction("Liste", "Marka");
        }
    }
}