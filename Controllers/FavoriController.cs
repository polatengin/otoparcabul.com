using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class FavoriController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Urun(string ProductCode)
        {
            Member Uye = Session["Member"] as Member;

            if (!string.IsNullOrWhiteSpace(ProductCode))
            {
                Product Urun = dc.Products.First(p => p.ProductCode == ProductCode);

                if (dc.FavoriteProducts.FirstOrDefault(fp => fp.MemberID == Uye.ID && fp.FavoriteProductID == Urun.ID) == null)
                {
                    dc.FavoriteProducts.Add(new FavoriteProduct { MemberID = Uye.ID, FavoriteProductID = Urun.ID });

                    dc.SaveChanges();
                }
            }

            ViewData["FavoriteProducts"] = dc.FavoriteProducts.Where(f => f.MemberID == Uye.ID && f.Product.InStock > 0).ToList();

            ViewData["Title"] = "Oto Parça Bul - Favori Ürünlerim";

            return View();
        }

        public ActionResult Satici(string UrlName)
        {
            Member Uye = Session["Member"] as Member;

            if (!string.IsNullOrWhiteSpace(UrlName))
            {
                Member Satici = dc.Members.First(m => m.UrlName == UrlName);

                dc.FavoriteMembers.Add(new FavoriteMember { MemberID = Uye.ID, FavoriteMemberID = Satici.ID });

                dc.SaveChanges();
            }

            ViewData["FavoriteMembers"] = dc.FavoriteMembers.Where(f => f.MemberID == Uye.ID && f.Member.IsActive && f.Member.IsVerified).ToList();

            ViewData["Title"] = "Oto Parça Bul - Favori Satıcılarım";

            return View();
        }
    }
}