using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NereyiGezelim.Models;
using PagedList;
using PagedList.Mvc;


namespace NereyiGezelim.Controllers
{
    public class HomeController : Controller
    {
        geziDB db = new geziDB();
        // GET: Home
        public ActionResult Index(int Page=1)
        {

            
                var yer = db.yers.OrderByDescending(y => y.yerid).ToPagedList(Page, 3);
                return View(yer);
            
               
            

            
        }

        public ActionResult KategoriYer(int id)
        {
            var yerler = db.yers.Where(y => y.kategori.kategoriid == id).ToList();


            return View(yerler);
        }

        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.yers.Where(y => y.baslik.Contains(Ara)).ToList();

            return View(aranan.OrderByDescending(y => y.tarih));
        }

        public ActionResult SonYorumlar()
        {
            

            return View(db.yorums.OrderByDescending(y=>y.yorumid).Take(5));

        }

        public ActionResult PopulerYerler()
        {


            return View(db.yers.OrderByDescending(y =>y.okunma).Take(5));

        }

        public ActionResult YerDetay(int id)
        {
            var yer = db.yers.Where(y => y.yerid == id).SingleOrDefault();
            if (yer == null)
            {
                return HttpNotFound();
            }
            return View(yer);
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Bolgeler()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult KategoriPartial()
        {
            return View(db.kategoris.ToList());
        }

        public JsonResult YorumYap(string yorum,int Yerid)
        {
            var uyeid = Session["UyeId"];

            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.yorums.Add(new yorum { uyeid = Convert.ToInt32(uyeid), yerid = Yerid, icerik = yorum, tarih = DateTime.Now });
            db.SaveChanges();
            return Json(false,JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["UyeId"];
            var yorum = db.yorums.Where(y => y.yorumid == id).SingleOrDefault();
            var yer=db.yers.Where(y=>y.yerid==yorum.yerid).SingleOrDefault();
            if (yorum.uyeid == Convert.ToInt32(uyeid))
            {
                db.yorums.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("YerDetay","Home",new { id = yer.yerid });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult OkunmaArttir(int Yerid)
        {

            var yer = db.yers.Where(y => y.yerid == Yerid).SingleOrDefault();
            if (yer.okunma != null)
            {
                yer.okunma += 1;
            }
            else
            {
                yer.okunma = 0;
                yer.okunma += 1;
            }
            db.SaveChanges();
            return View();
        }
    }
}