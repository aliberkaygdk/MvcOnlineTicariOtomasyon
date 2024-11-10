using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Cariler.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis = c.SatisHareket.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;
            var toplamtutar = c.SatisHareket.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;
            var toplamurunsayisi = c.SatisHareket.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamurunsayisi = toplamurunsayisi;
            var adsoyad = c.Cariler.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var sehir = c.Cariler.Where(x => x.CariMail == mail).Select(y=>y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehir;

            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = Session["CariMail"].ToString();
            var id = c.Cariler.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHareket.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = Session["CariMail"].ToString();
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(c => c.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(c => c.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = Session["CariMail"].ToString();
            var mesajlar = c.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(z => z.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(c => c.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(c => c.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var mail = Session["CariMail"].ToString();

            var gelensayisi = c.Mesajlars.Count(c => c.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(c => c.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(degerler);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gönderici = mail;
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));

            return View(k.ToList());
        }
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakip.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Cariler.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var caribul = c.Cariler.Find(id);
            return PartialView("Partial1", caribul);
        }
        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gönderici == "admin").OrderByDescending(y=>y.MesajID).ToList();
            return PartialView(veriler);
        }
        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Cariler.Find(cr.Cariid);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariSifre = cr.CariSifre;
            cari.CariSehir = cr.CariSehir;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}