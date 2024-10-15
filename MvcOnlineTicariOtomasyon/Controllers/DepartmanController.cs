using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using WebGrease.Activities;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Departman.Where(x=>x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
          return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            c.Departman.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var dep = c.Departman.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var dpt = c.Departman.Find(id);
            return View("DepartmanGetir",dpt);
        }
        public ActionResult DepartmanGuncelle(Departman p)
        {
            var dept = c.Departman.Find(p.Departmanid);
            dept.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personel.Where(x => x.Departmanid == id).ToList();
            ViewBag.dpt = c.Departman.Find(id).DepartmanAd;
           // hoca aşşağıdaki gibi yaptı
           //var dpt = c.Departman.Where(x=>x.Departmanid == id).Select(y=>y.DepartmanAd).FirstOrDefault();
           //ViewBag.d=dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHareket.Where(x=>x.Personelid== id).ToList();
            var per = c.Personel.Where(x=>x.Personelid == id).Select(y=>y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(degerler);
        }

    }
}