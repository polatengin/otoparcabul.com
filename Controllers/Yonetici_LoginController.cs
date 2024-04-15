using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class Yonetici_LoginController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Giris(string EMail, string Password)
        {
            Member Admin = dc.Members.FirstOrDefault(m => m.EMail == EMail && m.Password == Password && m.IsActive && m.IsVerified && m.IsAdmin);

            if (Admin != null)
            {
                Session["Admin"] = Admin;

                return Redirect("/yonetici/menu");
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Cikis()
        {
            Session.Clear();
            Session.Abandon();

            return Redirect("/");
        }

        public ActionResult Menu()
        {
            if (Session["Admin"] == null)
            {
                return Redirect("/");
            }

            return View();
        }
    }
}