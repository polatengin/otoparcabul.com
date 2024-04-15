using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace www.otoparcabul.com.Models
{
    public class HtmlHelpers
    {
        public static DateTime ResetDay(DateTime Date)
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);
        }

        public static string BrandListForForm()
        {
            StringBuilder sb = new StringBuilder(1000);

            foreach (var Marka in HttpContext.Current.Application["Brands"] as List<Brand>)
            {
                sb.AppendFormat("<option value=\"{0}\">{1} - {2}</option>", Marka.ID, Marka.VehicleType.Name, Marka.Name);
            }

            return sb.ToString();
        }

        public static string BrandListForSearch()
        {
            StringBuilder sb = new StringBuilder(1000);

            sb.Append("<option value='0'>Tüm Markalar</option>");

            foreach (var Marka in HttpContext.Current.Application["Brands"] as List<Brand>)
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", Marka.ID, Marka.Name);
            }

            return sb.ToString();
        }

        public static IEnumerable<SelectListItem> CityListForSearch()
        {
            yield return new SelectListItem { Text = "Tüm Şehirler", Value = "0" };

            foreach (var Sehir in HttpContext.Current.Application["Cities"] as List<City>)
            {
                yield return new SelectListItem { Text = Sehir.Name, Value = Sehir.ID.ToString() };
            }
        }

        public static IEnumerable<SelectListItem> CityListForForm(int CityID)
        {
            foreach (var Sehir in HttpContext.Current.Application["Cities"] as List<City>)
            {
                yield return new SelectListItem { Selected = (CityID == Sehir.ID), Text = Sehir.Name, Value = Sehir.ID.ToString() };
            }
        }

        public static string CategoryListForSearch()
        {
            StringBuilder sb = new StringBuilder(1000);

            sb.Append("<option value='0'>Tüm Kategoriler</option>");

            List<Category> Kategoriler = HttpContext.Current.Application["Categories"] as List<Category>;

            foreach (var UstKategori in Kategoriler.Where(k => k.IsActive && k.ParentCategoryID == 0))
            {
                sb.AppendFormat("<optgroup label=\"{0}\">", UstKategori.Name);

                foreach (var AltKategori in Kategoriler.Where(k => k.IsActive && k.ParentCategoryID == UstKategori.ID))
                {
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", AltKategori.ID, AltKategori.Name);
                }

                sb.Append("</optgroup>");
            }

            return sb.ToString();
        }

        public static string CategoryListForForm(int CategoryID = 0)
        {
            StringBuilder sb = new StringBuilder(1000);

            List<Category> Kategoriler = HttpContext.Current.Application["Categories"] as List<Category>;

            foreach (var UstKategori in Kategoriler.Where(k => k.IsActive && k.ParentCategoryID == 0))
            {
                sb.AppendFormat("<optgroup label=\"{0}\">", UstKategori.Name);

                foreach (var AltKategori in Kategoriler.Where(k => k.IsActive && k.ParentCategoryID == UstKategori.ID))
                {
                    sb.AppendFormat("<option value=\"{0}\"{2}>{1}</option>", AltKategori.ID, AltKategori.Name, CategoryID == AltKategori.ID ? " selected=\"selected\"" : "");
                }

                sb.Append("</optgroup>");
            }

            return sb.ToString();
        }

        public static string VehicleTypeListForForm(int VehicleTypeID = 0)
        {
            StringBuilder sb = new StringBuilder(1000);

            List<VehicleType> AracTipleri = HttpContext.Current.Application["VehicleTypes"] as List<VehicleType>;

            foreach (var AracTip in AracTipleri.Where(e => e.IsActive))
            {
                sb.AppendFormat("<option value=\"{0}\"{2}>{1}</option>", AracTip.ID, AracTip.Name, VehicleTypeID == AracTip.ID ? " selected=\"selected\"" : "");
            }

            return sb.ToString();
        }

        public static string ParentCategoryList()
        {
            StringBuilder sb = new StringBuilder(1000);

            sb.Append("<option value=\"0\">Ana Kategori</option>");

            List<Category> Kategoriler = HttpContext.Current.Application["Categories"] as List<Category>;

            foreach (var UstKategori in Kategoriler.Where(k => k.IsActive && k.ParentCategoryID == 0))
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", UstKategori.ID, UstKategori.Name);
            }

            return sb.ToString();
        }

        public static string ToUrlName(string Name)
        {
            Name = Name.ToLower().Trim().Replace("ı", "i").Replace("ş", "s").Replace("ö", "o").Replace("ğ", "g").Replace("ç", "c").Replace("ü", "u").Replace("*", "-").Replace("(", "-").Replace(")", "-").Replace("?", "-").Replace("+", "-").Replace("=", "-").Replace("&", "-");
            while (Name.IndexOf(" ") > 0)
            {
                Name = Name.Replace(" ", "-");
            }
            while (Name.IndexOf("--") > 0)
            {
                Name = Name.Replace("--", "-");
            }
            while (Name.EndsWith("-"))
            {
                Name = Name.Substring(0, Name.Length - 1);
            }
            return Name;
        }
    }
}