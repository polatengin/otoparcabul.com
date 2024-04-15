using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace www.otoparcabul.com
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Giris", "Yonetici/Giris", new { controller = "Yonetici_Login", action = "Giris" });
            routes.MapRoute("Cikis", "Yonetici/Cikis", new { controller = "Yonetici_Login", action = "Cikis" });
            routes.MapRoute("Menu", "Yonetici/Menu", new { controller = "Yonetici_Login", action = "Menu" });

            routes.MapRoute("YoneticiEMail", "Yonetici/EMail/Form", new { controller = "Yonetici_EMail", action = "Form" });

            routes.MapRoute("Urunler", "Yonetici/Urunler", new { controller = "Yonetici_Urun", action = "Liste" });
            routes.MapRoute("Son50Urun", "Yonetici/Urunler/Son50", new { controller = "Yonetici_Urun", action = "UrunSon50Liste" });

            routes.MapRoute("KullaniciDetay", "Yonetici/Kullanici/Detay/{MemberID}", new { controller = "Yonetici_Kullanici", action = "Detay" });

            routes.MapRoute("AracTipleriListe", "Yonetici/AracTipleri/Liste", new { controller = "Yonetici_AracTipleri", action = "Liste" });
            routes.MapRoute("AracTipleriKaydet", "Yonetici/AracTipleri/Kaydet", new { controller = "Yonetici_AracTipleri", action = "Kaydet" });

            routes.MapRoute("Saticilar", "Yonetici/Satici/Liste", new { controller = "Yonetici_Kullanici", action = "SaticiListe" });
            routes.MapRoute("SaticiKaydet", "Yonetici/Satici/Kaydet", new { controller = "Yonetici_Kullanici", action = "Kaydet" });
            routes.MapRoute("Son50Satici", "Yonetici/Satici/Son50", new { controller = "Yonetici_Kullanici", action = "SaticiSon50Liste" });

            routes.MapRoute("Magazalar", "Yonetici/Magaza/Liste", new { controller = "Yonetici_Kullanici", action = "MagazaListe" });
            routes.MapRoute("MagazaKaydet", "Yonetici/Magaza/Kaydet", new { controller = "Yonetici_Kullanici", action = "Kaydet" });
            routes.MapRoute("MagazaSon50", "Yonetici/Magaza/Son50", new { controller = "Yonetici_Kullanici", action = "MagazaSon50Liste" });

            routes.MapRoute("GununUrunleri", "Yonetici/gunun-urunleri/{Tarih}", new { controller = "Yonetici_GununUrunleri", action = "Index" });
            routes.MapRoute("GununUrunleriEkle", "Yonetici/Urun/Gunun-Urunu", new { controller = "Yonetici_GununUrunleri", action = "UrunEkle" });

            routes.MapRoute("VitrinUrunleri", "Yonetici/vitrin-urunleri/{Tarih}", new { controller = "Yonetici_VitrinUrunleri", action = "Index" });
            routes.MapRoute("VitrinUrunleriEkle", "Yonetici/Urun/Vitrin-Urunu", new { controller = "Yonetici_VitrinUrunleri", action = "UrunEkle" });

            routes.MapRoute("VitrinMagazalariEkle", "Yonetici/vitrinmagazalari/MagazaEkle", new { controller = "Yonetici_VitrinMagazalari", action = "MagazaEkle" });
            routes.MapRoute("VitrinMagazalari", "Yonetici/vitrin-magazalari/{Tarih}", new { controller = "Yonetici_VitrinMagazalari", action = "Index" });

            routes.MapRoute("YoneticiKategoriListe", "Yonetici/Kategori/Liste", new { controller = "Yonetici_Kategori", action = "Liste" });
            routes.MapRoute("YoneticiKategoriKaydet", "Yonetici/Kategori/Kaydet", new { controller = "Yonetici_Kategori", action = "Kaydet" });

            routes.MapRoute("YoneticiMarkaListe", "Yonetici/Marka/Liste", new { controller = "Yonetici_Marka", action = "Liste" });
            routes.MapRoute("YoneticiMarkaKaydet", "Yonetici/Marka/Kaydet", new { controller = "Yonetici_Marka", action = "Kaydet" });

            routes.MapRoute("YoneticiModelListe", "Yonetici/Model/Liste", new { controller = "Yonetici_Model", action = "Liste" });
            routes.MapRoute("YoneticiModelKaydet", "Yonetici/Model/Kaydet", new { controller = "Yonetici_Model", action = "Kaydet" });

            routes.MapRoute("YoneticiDefault", "Yonetici/{controller}/{action}", new { controller = "Yonetici_Login", action = "Index" });

            routes.MapRoute("AjaxMarkaList", "ajax/markalist/{VehicleTypeID}", new { controller = "Ajax", action = "GetBrandList" });
            routes.MapRoute("AjaxMarkaModelList", "ajax/markamodellist/{BrandID}", new { controller = "Ajax", action = "GetBrandModelList" });

            routes.MapRoute("SiteHaritasi", "SiteHaritasi", new { controller = "Static", action = "SiteHaritasi" });
            routes.MapRoute("KullanimKosullari", "kullanim-kosullari", new { controller = "Static", action = "KullanimKosullari" });
            routes.MapRoute("GizlilikPolitikasi", "gizlilik-politikasi", new { controller = "Static", action = "GizlilikPolitikasi" });

            routes.MapRoute("UrunAra", "urun/ara", new { controller = "Urun", action = "Ara", });
            routes.MapRoute("Urun", "urun/{ProductCode}/{UrlName}", new { controller = "Urun", action = "Index", ProductCode = "", UrlName = "" });

            routes.MapRoute("Ara", "ara", new { controller = "AnaSayfa", action = "Ara" });
            routes.MapRoute("Arama", "arama", new { controller = "AnaSayfa", action = "Arama" });

            routes.MapRoute("AlisVerisSepetiListe", "sepet", new { controller = "Sepet", action = "Index" });
            routes.MapRoute("AlisVerisSepetiSatinAl", "sepet/satin-al", new { controller = "Sepet", action = "SatinAl" });
            routes.MapRoute("AlisVerisSepetiEkle", "sepet/{ProductCode}", new { controller = "Sepet", action = "Ekle" });
            routes.MapRoute("AlisVerisSepetiSil", "sepet/sil/{ProductCode}", new { controller = "Sepet", action = "Sil" });

            routes.MapRoute("KategoriListe", "kategori/liste", new { controller = "Kategori", action = "Liste" });
            routes.MapRoute("KategoriIndex", "kategori/{UrlName}", new { controller = "Kategori", action = "Index" });

            routes.MapRoute("MarkaListe", "marka/liste", new { controller = "Marka", action = "Liste" });
            routes.MapRoute("MarkaIndex", "marka/{UrlName}", new { controller = "Marka", action = "Index" });

            routes.MapRoute("GununFirsatlari", "gunun-firsatlari", new { controller = "Kategori", action = "GununFirsatlari" });

            routes.MapRoute("Favorilerim", "favori/{action}/{UrlName}", new { controller = "Favori", action = "Urun", UrlName = UrlParameter.Optional });

            routes.MapRoute("MagazaListe", "magaza/liste", new { controller = "Uye", action = "MagazaListe" });
            routes.MapRoute("MagazaIndex", "magaza/{UrlName}/{Page}", new { controller = "Uye", action = "MagazaIndex", Page = 1 });
            routes.MapRoute("SaticiIndex", "satici/{UrlName}/{Page}", new { controller = "Uye", action = "MagazaIndex", Page = 1 });

            routes.MapRoute("UrunListesiStoktaVar", "uye/urunlistesi/stoktavar", new { controller = "Uye", action = "UrunListesiStoktaVar" });
            routes.MapRoute("UrunListesiStoktaYok", "uye/urunlistesi/stoktayok", new { controller = "Uye", action = "UrunListesiStoktaYok" });
            routes.MapRoute("UrunListesiEkle", "uye/urunlistesi/ekle", new { controller = "Uye", action = "UrunEkle" });
            routes.MapRoute("UrunDuzenle", "uye/urunduzenle/{ID}", new { controller = "Uye", action = "UrunDuzenle" });
            routes.MapRoute("UrunSil", "uye/urunsil/{ID}", new { controller = "Uye", action = "UrunSil" });
            routes.MapRoute("UrunSatildi", "uye/urunsatildi/{ID}", new { controller = "Uye", action = "UrunSatildi" });

            routes.MapRoute("UyeMesajListesiOkunmamis", "uye/mesajlistesi/okunmamis", new { controller = "Uye", action = "MesajListesiOkunmamis" });
            routes.MapRoute("UyeMesajListesiOkunmus", "uye/mesajlistesi/okunmus", new { controller = "Uye", action = "MesajListesiOkunmus" });
            routes.MapRoute("UyeDogrulama", "uye/dogrulama/{VerificationCode}/{EMail}", new { controller = "Uye", action = "Dogrulama" });
            routes.MapRoute("UyeSifreGuncelleme", "uye/sifre-guncelle", new { controller = "Uye", action = "SifreGuncelle" });
            routes.MapRoute("UyeProfilResimGuncelleme", "uye/profil-resim-guncelle", new { controller = "Uye", action = "ProfilResimGuncelle" });
            routes.MapRoute("UyeAdresGuncelleme", "uye/adres-guncelle", new { controller = "Uye", action = "AdresGuncelle" });
            routes.MapRoute("UyeAdSoyadGuncelleme", "uye/adsoyad-guncelle", new { controller = "Uye", action = "AdSoyadGuncelle" });

            routes.MapRoute("MesajGonder", "MesajGonder", new { controller = "Uye", action = "MesajGonder" });

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "AnaSayfa", action = "Index" });
        }
    }
}