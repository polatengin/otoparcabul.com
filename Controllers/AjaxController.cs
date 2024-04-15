using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class AjaxController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult EmailVerification(string EMail)
        {
            bool EMailUygun = dc.Members.FirstOrDefault(m => m.EMail == EMail.Trim()) == null;

            return Json(EMailUygun, JsonRequestBehavior.AllowGet);
        }

        public ContentResult GetBrandList(int VehicleTypeID)
        {
            StringBuilder sb = new StringBuilder(5000);

            foreach (var Entity in (HttpContext.Application["Brands"] as List<Brand>).Where(e => e.VehicleTypeID == VehicleTypeID))
            {
                sb.AppendFormat("<option value='{0}'>{1}</option>", Entity.ID, Entity.Name);
            }

            return Content(sb.ToString());
        }

        public ContentResult GetBrandModelList(int BrandID)
        {
            StringBuilder sb = new StringBuilder(5000);

            foreach (var Entity in (HttpContext.Application["BrandModels"] as List<BrandModel>).Where(e => e.BrandID == BrandID))
            {
                sb.AppendFormat("<option value='{0}'>{1}</option>", Entity.ID, Entity.Name);
            }

            return Content(sb.ToString());
        }
    }
}