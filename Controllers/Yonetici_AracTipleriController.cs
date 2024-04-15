using System;
using System.Linq;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class Yonetici_AracTipleriController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Liste()
        {
            if (Request["Delete"] != null)
            {
                int EntityID = Convert.ToInt32(Request["Delete"]);

                var Silinecek = dc.VehicleTypes.First(c => c.ID == EntityID);

                Silinecek.IsActive = false;

                dc.SaveChanges();

                HttpContext.Application["VehicleTypes"] = dc.VehicleTypes.OrderBy(e => e.Name).ToList();
            }

            if (Request["ToggleVehicleType"] != null)
            {
                int EntityID = Convert.ToInt32(Request["ToggleVehicleType"]);

                var Silinecek = dc.VehicleTypes.First(c => c.ID == EntityID);

                Silinecek.IsActive = !Silinecek.IsActive;

                dc.SaveChanges();

                HttpContext.Application["VehicleTypes"] = dc.VehicleTypes.OrderBy(e => e.Name).ToList();
            }

            return View();
        }

        public ActionResult Kaydet(int VehicleTypeID, string Name)
        {
            VehicleType entity = null;

            if (VehicleTypeID == 0)
            {
                entity = new VehicleType();

                dc.VehicleTypes.Add(entity);
            }
            else
            {
                entity = dc.VehicleTypes.First(c => c.ID == VehicleTypeID);
            }

            entity.Name = Name;
            entity.UrlName = HtmlHelpers.ToUrlName(Name);
            entity.IsActive = true;

            dc.SaveChanges();

            HttpContext.Application["VehicleTypes"] = dc.VehicleTypes.OrderBy(e => e.Name).ToList();

            return RedirectToAction("Liste", "Yonetici_AracTipleri");
        }
    }
}