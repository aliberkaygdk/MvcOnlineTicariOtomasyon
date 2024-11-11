using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalar.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalar.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalar.Find(id);
            return View("FaturaGetir", fatura);
            // return View("FaturaGetir",fatura);

        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalar.Find(f.Faturaid);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.Saat=f.Saat;
            fatura.FaturaTarih = f.FaturaTarih;
            fatura.TeslimAlan = f.TeslimAlan;
            fatura.TeslimEden = f.TeslimEden;
            fatura.VergiDairesi = f.VergiDairesi;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalem.Where(x => x.Faturaid == id).ToList();
            
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalem.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalar.ToList();
            cs.deger2 = c.FaturaKalem.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime FaturaTarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] faturaKalems)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.FaturaTarih = FaturaTarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalar.Add(f);
            foreach (var x in faturaKalems)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid =f.Faturaid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalem.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı",JsonRequestBehavior.AllowGet);
        }
    }
}