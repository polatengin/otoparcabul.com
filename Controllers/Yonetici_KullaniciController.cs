using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class Yonetici_KullaniciController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult SaticiListe()
        {
            if (Request["Delete"] != null)
            {
                int UyeID = Convert.ToInt32(Request["Delete"]);

                var SilinecekUye = dc.Members.First(m => m.ID == UyeID);

                SilinecekUye.IsActive = false;

                dc.SaveChanges();
            }
            else if (Request["ConvertToStore"] != null)
            {
                int UyeID = Convert.ToInt32(Request["ConvertToStore"]);

                var SilinecekUye = dc.Members.First(m => m.ID == UyeID);

                SilinecekUye.IsStore = true;

                dc.SaveChanges();
            }

            ViewData["Uyeler"] = dc.Members.Where(m => m.IsActive && !m.IsStore).OrderByDescending(m => m.SignUpDate).ToList();

            return View();
        }

        public ActionResult SaticiSon50Liste()
        {
            ViewData["Uyeler"] = dc.Members.Where(m => m.IsActive && !m.IsStore).OrderByDescending(m => m.SignUpDate).Take(50).ToList();

            return View();
        }

        public ActionResult MagazaListe()
        {
            if (Request["Delete"] != null)
            {
                int UyeID = Convert.ToInt32(Request["Delete"]);

                var SilinecekUye = dc.Members.First(m => m.ID == UyeID);

                SilinecekUye.IsActive = false;

                dc.SaveChanges();
            }
            else if (Request["ConvertToMember"] != null)
            {
                int UyeID = Convert.ToInt32(Request["ConvertToMember"]);

                var SilinecekUye = dc.Members.First(m => m.ID == UyeID);

                SilinecekUye.IsStore = false;

                dc.SaveChanges();
            }

            ViewData["Uyeler"] = dc.Members.Where(m => m.IsActive && m.IsStore).OrderByDescending(m => m.SignUpDate).ToList();

            return View();
        }

        public ActionResult MagazaSon50Liste()
        {
            ViewData["Uyeler"] = dc.Members.Where(m => m.IsActive && m.IsStore).OrderByDescending(m => m.SignUpDate).Take(50).ToList();

            return View();
        }

        public ContentResult Detay(int MemberID)
        {
            Member Uye = dc.Members.FirstOrDefault(m => m.ID == MemberID);

            StringBuilder sb = new StringBuilder(1000);

            sb.Append("{");

            sb.AppendFormat("\"FirstName\": \"{0}\",", Uye.FirstName);
            sb.AppendFormat("\"LastName\": \"{0}\",", Uye.LastName);
            sb.AppendFormat("\"UrlName\": \"{0}\",", Uye.UrlName);
            sb.AppendFormat("\"EMail\": \"{0}\",", Uye.EMail);
            sb.AppendFormat("\"Password\": \"{0}\",", Uye.Password);
            sb.AppendFormat("\"IsStore\": \"{0}\",", Uye.IsStore);
            sb.AppendFormat("\"IsVerified\": \"{0}\",", Uye.IsVerified);
            sb.AppendFormat("\"VerificationCode\": \"{0}\",", Uye.VerificationCode);
            sb.AppendFormat("\"Address\": \"{0}\",", Uye.Address.Replace(Environment.NewLine, ""));
            sb.AppendFormat("\"Tel1\": \"{0}\",", Uye.Tel1);
            sb.AppendFormat("\"Tel2\": \"{0}\",", Uye.Tel2);
            sb.AppendFormat("\"Description\": \"{0}\",", Uye.Description);
            sb.AppendFormat("\"IsActive\": \"{0}\",", Uye.IsActive);
            sb.AppendFormat("\"IsAdmin\": \"{0}\"", Uye.IsAdmin);

            sb.Append("}");

            return Content(sb.ToString(), "text/json");
        }

        public RedirectToRouteResult Kaydet(int MemberID, string FirstName, string LastName, string UrlName, string EMail, string Password, string VerificationCode, string Address, string Tel1, string Tel2, string Description)
        {
            Member Uye = dc.Members.Where(m => m.ID == MemberID).First();

            Uye.FirstName = FirstName;
            Uye.LastName = LastName;
            Uye.UrlName = UrlName;
            Uye.EMail = EMail;
            Uye.Password = Password;
            Uye.IsStore = (Request["IsStore"] != null);
            Uye.IsVerified = (Request["IsVerified"] != null);
            Uye.VerificationCode = VerificationCode;
            Uye.Address = Address;
            Uye.Tel1 = Tel1;
            Uye.Tel2 = Tel2;
            Uye.IsActive = (Request["IsActive"] != null);
            Uye.IsAdmin = (Request["IsAdmin"] != null);

            dc.SaveChanges();

            string Yonlendirme = "SaticiListe";

            switch (Request.Path.Replace("/yonetici/", "").Replace("/kaydet", "").Trim())
            {
                case "magaza":
                    Yonlendirme = "MagazaListe";
                    break;
                case "satici":
                default:
                    Yonlendirme = "SaticiListe";
                    break;
            }

            return RedirectToAction(Yonlendirme);
        }
    }
}