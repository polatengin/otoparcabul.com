using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com.Controllers
{
    public class UyeController : Controller
    {
        OtoParcaBulEntities dc = new OtoParcaBulEntities();

        public ActionResult Cikis()
        {
            Session.Clear();
            Session.Abandon();

            return Redirect("/");
        }

        public ActionResult Dogrulama(string VerificationCode, string EMail)
        {
            Member Uye = dc.Members.FirstOrDefault(m => m.VerificationCode == VerificationCode && m.EMail == EMail && !m.IsVerified && m.IsActive);

            if (Uye != null)
            {
                Uye.IsVerified = true;
                Uye.Password = Guid.NewGuid().ToString().Substring(0, 8);
                Uye.LastModifiedDate = DateTime.Now;
            }

            MailHelper.SendMail(EMail, "Oto Parça Bul Üyelik Bilgileriniz", string.Format("Sayın {0} {1},<br /><br /><a href='http://www.otoparcabul.com/'>Oto Parça Bul</a> sitesinde oluşturduğunuz üyeliğiniz ile ilgili bilgileriniz aşağıdadır;<br /><br />Şifreniz: {2}", Uye.FirstName, Uye.LastName, Uye.Password));

            dc.SaveChanges();

            return Redirect("/");
        }

        public ActionResult Profil()
        {
            DateTime SoldDateLimit = DateTime.Now.AddDays(-15);

            Member Uye = Session["Member"] as Member;

            ViewData["Member"] = Uye;
            ViewData["Products"] = dc.Products.Where(e => e.MemberID == Uye.ID && e.InStock > 0 && !e.IsDeleted && (!e.SoldDate.HasValue || e.SoldDate.Value > SoldDateLimit)).ToList();

            ViewData["Title"] = "Oto Parça Bul - Üye : " + Uye.FullName;

            return View();
        }

        [ValidateInput(false)]
        public ActionResult MesajGonder(int Receiver, string Body)
        {
            Member Uye = Session["Member"] as Member;

            Message Mesaj = new Message { Body = Body, IsRead = false, ReceiverMemberID = Receiver, SenderMemberID = Uye.ID };

            dc.Messages.Add(Mesaj);

            dc.SaveChanges();

            return new EmptyResult();
        }

        public ActionResult MesajListesiOkunmamis()
        {
            Member Uye = Session["Member"] as Member;

            var messageList = dc.Messages.Where(m => m.ReceiverMemberID == Uye.ID && m.IsRead == false);

            if (messageList.Any())
            {
                foreach (var messageItem in messageList)
                {
                    messageItem.IsRead = true;
                }

                dc.SaveChanges();
            }

            ViewData["MessageList"] = messageList.ToList();

            ViewData["Title"] = "Oto Parça Bul - Üye Mesaj Liste : " + Uye.FullName;

            return View();
        }

        public ActionResult MesajListesiOkunmus()
        {
            Member Uye = Session["Member"] as Member;

            ViewData["MessageList"] = dc.Messages.Where(m => m.ReceiverMemberID == Uye.ID && m.IsRead == true).ToList();

            ViewData["Title"] = "Oto Parça Bul - Üye Mesaj Liste : " + Uye.FullName;

            return View();
        }

        public ActionResult MagazaIndex(string UrlName, int Page)
        {
            Member Magaza = dc.Members.FirstOrDefault(m => m.UrlName == UrlName && m.IsActive);

            ViewData["Store"] = Magaza;

            ViewData["Title"] = "Oto Parça Bul - Mağaza : " + Magaza.FullName;

            var Products = Magaza.Products.Where(p => !p.IsDeleted && p.InStock > 0).OrderByDescending(p => p.LastModifiedDate);

            ViewData["Pages"] = (Products.Count() / 20) + 1;
            ViewData["Products"] = Products.Skip((Page - 1) * 20).Take(20).ToList();

            return View();
        }

        public ActionResult MagazaListe()
        {
            ViewData["StoreList"] = dc.Members.Where(m => m.IsStore && m.IsActive).OrderByDescending(m => m.LastModifiedDate).ToList();

            ViewData["Title"] = "Oto Parça Bul - Mağaza Liste";

            return View();
        }

        public ActionResult Giris()
        {
            if (TempData["AlertMessage"] == null)
            {
                TempData["AlertMessage"] = "";
            }

            return View();
        }

        public ActionResult SifreHatirlat()
        {
            return View();
        }

        public ActionResult SifreGuncelle(string EskiSifre, string YeniSifre1, string YeniSifre2)
        {
            if (Session["Member"] != null)
            {
                Member Uye = Session["Member"] as Member;
                if (Uye.Password == EskiSifre && YeniSifre1 == YeniSifre2)
                {
                    Member GuncellenecekUye = dc.Members.First(m => m.ID == Uye.ID);
                    GuncellenecekUye.Password = YeniSifre1;

                    dc.SaveChanges();

                    Session["Member"] = GuncellenecekUye;
                }
            }

            return Redirect("/");
        }

        public ActionResult ProfilResimGuncelle(HttpPostedFileBase ProfileImage)
        {
            if (Session["Member"] != null)
            {
                Member Uye = Session["Member"] as Member;

                Member GuncellenecekUye = dc.Members.First(m => m.ID == Uye.ID);

                if (GuncellenecekUye.LogoFileName == null || GuncellenecekUye.LogoFileName == "")
                {
                    GuncellenecekUye.LogoFileName = Guid.NewGuid().ToString() + ".jpg";

                    dc.SaveChanges();

                    ViewData["Member"] = GuncellenecekUye;
                }

                new Bitmap(Image.FromStream(ProfileImage.InputStream), 150, 150).Save(Server.MapPath("/Content/m/" + GuncellenecekUye.LogoFileName), ImageFormat.Jpeg);
            }

            return Redirect("/");
        }

        public ActionResult AdresGuncelle(byte CityID, string Address, string Tel1, string Tel2)
        {
            if (Session["Member"] != null)
            {
                Member Uye = Session["Member"] as Member;

                Member GuncellenecekUye = dc.Members.First(m => m.ID == Uye.ID);
                GuncellenecekUye.CityID = CityID;
                GuncellenecekUye.Address = Address;
                GuncellenecekUye.Tel1 = Tel1;
                GuncellenecekUye.Tel2 = Tel2;

                dc.SaveChanges();

                Session["Member"] = GuncellenecekUye;
            }

            return Redirect("/");
        }

        public ActionResult AdSoyadGuncelle(string FirstName, string LastName)
        {
            if (Session["Member"] != null)
            {
                Member Uye = Session["Member"] as Member;

                Member GuncellenecekUye = dc.Members.First(m => m.ID == Uye.ID);
                GuncellenecekUye.FirstName = FirstName;
                GuncellenecekUye.LastName = LastName;

                dc.SaveChanges();

                Session["Member"] = GuncellenecekUye;
            }

            return Redirect("/");
        }

        public ActionResult UyeGiris(string EMail, string Password)
        {
            string ControllerName = "AnaSayfa";
            string ActionName = "Index";

            if (EMail != "" && Password != "")
            {
                Member Uye = dc.Members.FirstOrDefault(m => m.EMail == EMail && m.Password == Password && m.IsVerified && m.IsActive);

                if (Uye != null)
                {
                    Uye.LastLoginDate = DateTime.Now;

                    dc.SaveChanges();
                }
                else
                {
                    ControllerName = "Uye";
                    ActionName = "Giris";

                    TempData["AlertMessage"] = "$.jGrowl(\"EMail adresi ve Şifresi bilgilerini kontrol ediniz.\");";
                }

                Session["Member"] = Uye;

                return RedirectToAction(ActionName, ControllerName);
            }
            else
            {
                return RedirectToAction(ActionName, ControllerName);
            }
        }

        public ActionResult Kayit()
        {
            return View();
        }

        public ActionResult UyeKayit(string EMail, string FirstName, string LastName, string Address, string Tel1, string Tel2, byte CityID, HttpPostedFileBase LogoImage)
        {
            if (EMail != "" && FirstName != "" && LastName != "" && Tel1 != "")
            {
                string VerificationCode = Guid.NewGuid().ToString();

                Member Uye = new Member();
                Uye.EMail = EMail;
                Uye.FirstName = FirstName;
                Uye.LastName = LastName;
                Uye.UrlName = Guid.NewGuid().ToString();
                Uye.Password = "";
                Uye.Description = "";
                Uye.Address = Address;
                Uye.Tel1 = Tel1;
                Uye.Tel2 = Tel2;
                Uye.CityID = CityID;
                Uye.VerificationCode = VerificationCode;
                Uye.SignUpDate = DateTime.Now;
                Uye.LastModifiedDate = DateTime.Now;
                Uye.LastLoginDate = DateTime.Now;
                Uye.IsActive = true;

                if (LogoImage != null && LogoImage.ContentLength > 0)
                {
                    string LogoFileName = Guid.NewGuid().ToString() + ".jpg";

                    Uye.LogoFileName = LogoFileName;

                    new Bitmap(Image.FromStream(LogoImage.InputStream), 150, 150).Save(Server.MapPath("/Content/m/" + LogoFileName), ImageFormat.Jpeg);
                }

                MailHelper.SendMail(EMail, "Oto Parça Bul Üyelik Doğrulama Kodunuz", string.Format("Sayın {0} {1},<br /><br /><a href='http://www.otoparcabul.net/'>Oto Parça Bul</a> sitesinde oluşturduğunuz üyeliğinizi onaylamak için aşağıdaki linke tıklayınız;<br /><br /><a href='http://www.otoparcabul.net/uye/dogrulama/{2}/{3}'>{2}</a>", Uye.FirstName, Uye.LastName, VerificationCode, Uye.EMail));

                dc.Members.Add(Uye);

                dc.SaveChanges();
            }

            TempData["AlertMessage"] = "$.jGrowl(\"Kullanıcı bilgileriniz EMail adresinize gönderilmiştir, lütfen kontrol ediniz.\");";

            return RedirectToAction("Index", "AnaSayfa");
        }

        public ActionResult UrunListesiStoktaVar()
        {
            DateTime SoldDateLimit = DateTime.Now.AddDays(-15);

            Member Uye = Session["Member"] as Member;

            ViewData["ProductList"] = dc.Products.Where(p => p.InStock > 0 && !p.IsDeleted && p.MemberID == Uye.ID && (!p.SoldDate.HasValue || p.SoldDate.Value > SoldDateLimit)).OrderByDescending(p => p.LastModifiedDate).ToList();

            ViewData["Title"] = "Oto Parça Bul - Ürün Listesi / Stokta Var";

            return View();
        }

        public ActionResult UrunListesiStoktaYok()
        {
            DateTime SoldDateLimit = DateTime.Now.AddDays(-15);

            Member Uye = Session["Member"] as Member;

            ViewData["ProductList"] = dc.Products.Where(p => p.InStock == 0 && !p.IsDeleted && p.MemberID == Uye.ID && (!p.SoldDate.HasValue || p.SoldDate.Value > SoldDateLimit)).OrderByDescending(p => p.LastModifiedDate).ToList();

            ViewData["Title"] = "Oto Parça Bul - Ürün Listesi / Stokta Yok";

            return View();
        }

        public ActionResult UrunDuzenle(int ID)
        {
            Member Uye = Session["Member"] as Member;

            ViewData["Product"] = dc.Products.FirstOrDefault(e => e.ID == ID && e.MemberID == Uye.ID);

            return View();
        }

        public ActionResult UrunSil(int ID)
        {
            Member Uye = Session["Member"] as Member;

            dc.Products.FirstOrDefault(e => e.ID == ID && e.MemberID == Uye.ID && !e.IsDeleted).IsDeleted = true;

            dc.SaveChanges();

            return Redirect("/");
        }

        public ActionResult UrunSatildi(int ID)
        {
            Member Uye = Session["Member"] as Member;

            //dc.Products.FirstOrDefault(e => e.ID == ID && e.MemberID == Uye.ID && !e.IsDeleted).SoldDate = DateTime.Now;

            //dc.SaveChanges();

            return Redirect("/");
        }

        public ActionResult UrunEkle()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UrunKayit(int ID, int VehicleTypeID, int CategoryID, string Title, decimal Price, string IsNew, int InStock, int BrandID, int BrandModelID)
        {
            HttpContext.Server.ScriptTimeout = 60 * 30;

            Member SessionUye = Session["Member"] as Member;

            Member Uye = dc.Members.FirstOrDefault(m => m.ID == SessionUye.ID);

            Uye.LastModifiedDate = DateTime.Now;

            Product Urun = dc.Products.FirstOrDefault(e => e.ID == ID && e.MemberID == Uye.ID);

            if (Urun == null)
            {
                Urun = new Product();
                Urun.ID = 0;
                Urun.CreateDate = DateTime.Now;
                Urun.UrlName = Guid.NewGuid().ToString();
                Urun.ProductCode = HtmlHelpers.ToUrlName(Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 12);
                Urun.SoldDate = null;
            }
            try
            {
                Urun.CategoryID = CategoryID;
                Urun.MemberID = Uye.ID;
                Urun.Title = Title;
                Urun.Description = Request["Description"].Trim();
                Urun.VehicleTypeID = VehicleTypeID;
                Urun.BrandID = BrandID;
                Urun.BrandModelID = BrandModelID;
                Urun.Price = Price;
                Urun.IsNew = (IsNew == "New");
                Urun.CreatedStock = InStock;
                Urun.InStock = InStock;
                Urun.LastModifiedDate = DateTime.Now;

                if (Urun.ID == 0)
                {
                    dc.Products.Add(Urun);
                }
            }
            catch (Exception ex)
            {
                Log hata = new Log();

                hata.MemberID = Uye.ID;
                hata.Body = ex.Message;
                hata.Subject = "Product Insert";
                hata.CreatedDate = DateTime.Now;

                dc.Logs.Add(hata);

                throw;
            }

            dc.SaveChanges();

            var savedProduct = dc.Products.FirstOrDefault(e => e.ID == Urun.ID);
            savedProduct.Category = dc.Categories.FirstOrDefault(e => e.ID == Urun.CategoryID);
            savedProduct.VehicleType = dc.VehicleTypes.FirstOrDefault(e => e.ID == Urun.VehicleTypeID);
            savedProduct.Brand = dc.Brands.FirstOrDefault(e => e.ID == Urun.BrandID);
            savedProduct.BrandModel = dc.BrandModels.FirstOrDefault(e => e.ID == Urun.BrandModelID);

            ViewData["Resimler"] = new List<ProductImage>();

            return View(savedProduct);
        }

        public ActionResult UrunResimEkle(int UrunID)
        {
            HttpContext.Server.ScriptTimeout = 60 * 30;

            Member SessionUye = Session["Member"] as Member;

            Member Uye = dc.Members.FirstOrDefault(m => m.ID == SessionUye.ID);

            Product Urun = dc.Products.FirstOrDefault(e => e.ID == UrunID);

            HttpPostedFileBase Image = Request.Files["Image"];

            if (Image != null && Image.ContentLength > 0)
            {
                try
                {
                    string FileName = Guid.NewGuid().ToString() + ".jpg";

                    ProductImage pi = new ProductImage();
                    pi.ProductID = Urun.ID;
                    pi.MegaFileName = FileName;
                    pi.FileName = FileName;
                    pi.ThumbnailFileName = FileName;

                    ProcessProductImage(Image, FileName);

                    dc.ProductImages.Add(pi);
                }
                catch (Exception ex)
                {
                    Log hata = new Log();

                    hata.MemberID = Uye.ID;
                    hata.Body = ex.Message;
                    hata.Subject = "Product Image Insert";
                    hata.CreatedDate = DateTime.Now;

                    dc.Logs.Add(hata);
                }

                dc.SaveChanges();
            }

            ViewData["Resimler"] = dc.ProductImages.Where(e => e.ProductID == Urun.ID).ToList();

            return View("UrunKayit", Urun);
        }

        [NonAction]
        private void ProcessProductImage(HttpPostedFileBase ProductImage, string FileName)
        {
            HttpContext.Server.ScriptTimeout = 60 * 30;

            Image Resim = Image.FromStream(ProductImage.InputStream, false);

            Graphics g = Graphics.FromImage(Resim);
            g.DrawString("http://www.otoparcabul.com", new Font("Verdana", 16), Brushes.Black, new Point(12, Resim.Height - 33));
            g.DrawString("http://www.otoparcabul.com", new Font("Verdana", 16), Brushes.White, new Point(10, Resim.Height - 35));

            Resim.Save(Server.MapPath("/Content/p/m/" + FileName), ImageFormat.Jpeg);

            var Resim600x400 = new Bitmap(Resim, 600, 400);
            Graphics g600x400 = Graphics.FromImage(Resim600x400);
            g.DrawString("http://www.otoparcabul.com", new Font("Verdana", 16), Brushes.Black, new Point(12, Resim.Height - 33));
            g.DrawString("http://www.otoparcabul.com", new Font("Verdana", 16), Brushes.White, new Point(10, Resim.Height - 35));

            Resim600x400.Save(Server.MapPath("/Content/p/n/" + FileName), ImageFormat.Jpeg);

            new Bitmap(Resim600x400, 225, 150).Save(Server.MapPath("/Content/p/t/" + FileName), ImageFormat.Jpeg);
        }
    }
}