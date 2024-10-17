using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Cariler.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = c.Urun.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = c.Personel.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = c.Kategories.Count().ToString();
            ViewBag.d4 = deger4;
            var deger5 = c.Urun.Sum(x=>x.Stok).ToString();
            ViewBag.d5 = deger5;
            var deger6 = (from x in c.Urun select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;
            var deger7 = c.Urun.Count(x => x.Stok<=20).ToString();
            ViewBag.d7 = deger7;
            var deger8 = (from x in c.Urun orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;
            var deger9 = (from x in c.Urun orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;
            var deger10 = c.Urun.Count(x => x.UrunAd=="Buzdolabı").ToString();
            ViewBag.d10 = deger10;
            var deger11 = c.Urun.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;
            var deger12=c.Urun.GroupBy(x=>x.Marka).OrderByDescending(z=>z.Count()).Select(y=>y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;
            var deger13 = c.Urun.Where(u => u.Urunid == (c.SatisHareket.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;

            var deger14 = c.SatisHareket.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;
            var deger15 = c.SatisHareket.Count(x=>x.Tarih==DateTime.Today).ToString();
            ViewBag.d15 = deger15;
            if (Convert.ToInt32(deger15) != 0)

            {

                var deger16 = c.SatisHareket.Where(x => x.Tarih == DateTime.Today).Sum(y => y.ToplamTutar).ToString();

                ViewBag.d16 = deger16;

            }

            else { ViewBag.d16 = deger15; }
            // var deger16 = c.SatisHareket.Where(x => x.Tarih == DateTime.Today).Sum(y => y.ToplamTutar).ToString();//
           // ViewBag.d16 = deger15;
            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = from x in c.Cariler
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu = from x in c.Personel
                        group x by x.Departman.DepartmanAd into g
                        select new SinifGrup2
                        {
                            Departman = g.Key,
                            Sayi = g.Count()
                        };

            return PartialView(sorgu.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu = c.Cariler.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial3()
        {
            var sorgu = c.Urun.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial4()
        {
            var sorgu = from x in c.Urun
                        group x by x.Marka into g
                        select new SinifGrup3
                        {
                            marka = g.Key,
                            sayi = g.Count()
                        };

            return PartialView(sorgu.ToList());
        }
    }
}