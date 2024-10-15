using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Urun.Where(x=>x.Durum == true).ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategories.ToList() select new SelectListItem
            {
                Text = x.KategoriAd,
                Value = x.KategoriID.ToString()
            }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Urun.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = c.Urun.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var urundeger = c.Urun.Find(id);

            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urun = c.Urun.Find(p.Urunid);
            urun.AlisFiyat=p.AlisFiyat;
            urun.SatisFiyat = p.SatisFiyat;
            urun.Durum = p.Durum;
            urun.Marka = p.Marka;
            urun.UrunAd = p.UrunAd;
            urun.UrunGorsel = p.UrunGorsel;
            urun.Stok = p.Stok;
            urun.Kategoriid = p.Kategoriid;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}