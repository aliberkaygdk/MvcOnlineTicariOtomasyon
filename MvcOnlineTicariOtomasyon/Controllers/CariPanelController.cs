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
    }
}