using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = Session["CariMail"].ToString();
            var degerler = c.Cariler.FirstOrDefault(X=> X.CariMail==mail);
            ViewBag.m = mail;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var mail = Session["CariMail"].ToString();
            var id = c.Cariler.Where(x => x.CariMail == mail).Select(y=>y.Cariid).FirstOrDefault();
            var degerler = c.SatisHareket.Where(x=>x.Cariid==id).ToList();
            return View(degerler);
        }
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
    }
}