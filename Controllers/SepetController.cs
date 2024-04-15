using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class SepetController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Index()
        {
            Member Uye = Session["Member"] as Member;

            ViewData["Title"] = "Oto Parça Bul - Alış-Veriş Sepetim";

            ViewData["ShoppingCartDetails"] = dc.ShoppingCartDetails.Where(scd => scd.ShoppingCart.MemberID == Uye.ID && scd.ShoppingCart.Status == 1).ToList();

            return View();
        }

        public ActionResult Ekle(string ProductCode)
        {
            Member Uye = Session["Member"] as Member;

            if (!string.IsNullOrWhiteSpace(ProductCode))
            {
                Product Urun = dc.Products.First(p => p.ProductCode == ProductCode);

                ShoppingCart Sepet = dc.ShoppingCarts.FirstOrDefault(sc => sc.MemberID == Uye.ID && sc.Status == 1);

                if (Sepet == null)
                {
                    Sepet = new ShoppingCart { MemberID = Uye.ID, Status = 1, CreateDate = DateTime.Now };

                    dc.ShoppingCarts.Add(Sepet);
                }

                ShoppingCartDetail SepeteEklenecekUrun = dc.ShoppingCartDetails.FirstOrDefault(scd => scd.ShoppingCartID == Sepet.ID && scd.ProductID == Urun.ID);

                if (SepeteEklenecekUrun == null)
                {
                    dc.ShoppingCartDetails.Add(new ShoppingCartDetail { ShoppingCartID = Sepet.ID, ProductID = Urun.ID, Unit = 1 });
                }
                else if (SepeteEklenecekUrun.Unit < Urun.InStock)
                {
                    SepeteEklenecekUrun.Unit += 1;
                }

                dc.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult SatinAl()
        {
            Member Uye = Session["Member"] as Member;

            ViewData["ItemCount"] = dc.ShoppingCartDetails.Where(scd => scd.ShoppingCart.MemberID == Uye.ID && scd.ShoppingCart.Status == 1).Sum(scd => scd.Unit);

            ViewData["TotalCash"] = dc.ShoppingCartDetails.Where(scd => scd.ShoppingCart.MemberID == Uye.ID && scd.ShoppingCart.Status == 1).Sum(scd => scd.Product.Price * scd.Unit);

            ViewData["Title"] = "Oto Parça Bul - Alış-Veriş Sepetim / Satın Al";

            return View();
        }

        public ActionResult Sil(string ProductCode)
        {
            Member Uye = Session["Member"] as Member;

            if (!string.IsNullOrWhiteSpace(ProductCode))
            {
                Product Urun = dc.Products.First(p => p.ProductCode == ProductCode);

                ShoppingCart Sepet = dc.ShoppingCarts.FirstOrDefault(sc => sc.MemberID == Uye.ID && sc.Status == 1);

                if (Sepet == null)
                {
                    Sepet = new ShoppingCart { MemberID = Uye.ID, Status = 1, CreateDate = DateTime.Now };

                    dc.ShoppingCarts.Add(Sepet);
                }

                ShoppingCartDetail SepeteEklenecekUrun = dc.ShoppingCartDetails.FirstOrDefault(scd => scd.ShoppingCartID == Sepet.ID && scd.ProductID == Urun.ID);
                if (SepeteEklenecekUrun != null)
                {
                    dc.ShoppingCartDetails.Remove(SepeteEklenecekUrun);
                }

                dc.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}