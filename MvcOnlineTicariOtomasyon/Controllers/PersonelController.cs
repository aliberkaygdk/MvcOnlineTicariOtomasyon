using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler= c.Personel.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departman.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            c.Personel.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departman.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            

            var prs = c.Personel.Find(id);
            return View("PersonelGetir",prs);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            var prsn = c.Personel.Find(p.Personelid);
            prsn.PersonelAd=p.PersonelAd;
            prsn.PersonelSoyad=p.PersonelSoyad;
            prsn.PersonelGorsel=p.PersonelGorsel;
            prsn.Departmanid = p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}