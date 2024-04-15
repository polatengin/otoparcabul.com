using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class AnaSayfaController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Index()
        {
            DateTime Bugun = HtmlHelpers.ResetDay(DateTime.Now);
            DateTime SoldDateLimit = DateTime.Now.AddDays(-15);

            ViewData["ShowRoomProducts"] = dc.ShowRooms.Where(sh => sh.Date == Bugun).Select(sh => sh.Product).Where(p => !p.IsDeleted && p.InStock > 0).OrderByDescending(p => p.LastModifiedDate).Take(50).ToList();

            ViewData["ShowStoreMembers"] = dc.ShowStores.Where(sh => sh.Date == Bugun).Select(sh => sh.Member).Where(p => p.IsActive && p.IsStore && p.IsVerified).OrderByDescending(p => p.LastModifiedDate).Take(50).ToList();

            ViewData["LastModifiedProducts"] = dc.Products.Where(p => !p.IsDeleted && p.InStock > 0 && (!p.SoldDate.HasValue || p.SoldDate.Value > SoldDateLimit)).OrderByDescending(p => p.LastModifiedDate).Take(30).ToList();

            ViewData["DailyChances"] = dc.DailyChances.Where(d => d.Date == Bugun).OrderByDescending(d => d.Product.InStock).Take(12).ToList();

            ViewData["Title"] = "Oto Parça Bul - Ana Sayfa";

            if (TempData["AlertMessage"] == null)
            {
                TempData["AlertMessage"] = "";
            }

            return View();
        }

        public ActionResult Ara(string Aciklama)
        {
            DateTime SoldDateLimit = DateTime.Now.AddDays(-15);
            if (Aciklama.Trim() != "")
            {
                var Urunler = dc.Products.Where(p => !p.IsDeleted && p.InStock > 0 && (!p.SoldDate.HasValue || p.SoldDate.Value > SoldDateLimit) && (p.Title.Contains(Aciklama.Trim()) || p.Description.Contains(Aciklama.Trim()) || p.ProductCode.Contains(Aciklama.Trim()))).Take(50).OrderByDescending(p => p.LastModifiedDate).ToList();

                ViewData["Urunler"] = Urunler;
                ViewData["Title"] = "Oto Parça Bul - Arama Sonuçları";

                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Arama(int Durum, int Sehir, int Fiyat, int Kategori, int Marka, string Aciklama, string UrunKodu)
        {
            DateTime SoldDateLimit = DateTime.Now.AddDays(-15);

            var Urunler = dc.Products.Where(p => !p.IsDeleted && p.InStock > 0 && (!p.SoldDate.HasValue || p.SoldDate.Value > SoldDateLimit));

            if (UrunKodu.Length > 0)
            {
                Urunler = Urunler.Where(p => p.ProductCode == UrunKodu);
            }
            else
            {
                if (Durum == 1)
                {
                    Urunler = Urunler.Where(p => p.IsNew);
                }
                else if (Durum == 2)
                {
                    Urunler = Urunler.Where(p => !p.IsNew);
                }

                if (Sehir > 0)
                {
                    Urunler = Urunler.Where(p => p.Member.CityID == Sehir);
                }

                switch (Fiyat)
                {
                    case 10:
                        Urunler = Urunler.Where(p => p.Price <= 50);
                        break;
                    case 20:
                        Urunler = Urunler.Where(p => p.Price >= 50 && p.Price <= 150);
                        break;
                    case 30:
                        Urunler = Urunler.Where(p => p.Price >= 150 && p.Price <= 250);
                        break;
                    case 40:
                        Urunler = Urunler.Where(p => p.Price >= 250 && p.Price <= 500);
                        break;
                    case 50:
                        Urunler = Urunler.Where(p => p.Price >= 500);
                        break;
                }

                if (Kategori > 0)
                {
                    Urunler = Urunler.Where(p => p.CategoryID == Kategori);
                }

                if (Marka > 0)
                {
                    // TODO
                }

                if (Aciklama.Trim() != "")
                {
                    Urunler = Urunler.Where(p => p.Title.Contains(Aciklama.Trim()) || p.Description.Contains(Aciklama.Trim()));
                }
            }

            var Sonuc = Urunler.Take(50).OrderByDescending(p => p.LastModifiedDate).ToList();

            ViewData["Urunler"] = Sonuc;
            ViewData["Title"] = "Oto Parça Bul - Arama Sonuçları";

            return View();
        }
    }
}