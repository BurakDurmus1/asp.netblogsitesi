using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NereyiGezelim.Models;
using System.IO;
using System.Web.Helpers;

namespace NereyiGezelim.Controllers
{

    public class UyeController : Controller
    {
        geziDB db = new geziDB();


        public ActionResult Index(int id)
        {
            var Uye = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();


            if (Convert.ToInt32(Session["UyeId"])!=Uye.uyeid)
            {
                return HttpNotFound();
            }
            return View(Uye);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(uye Uye)
        {
            var login = db.uyes.Where(u => u.kullaniciadi == Uye.kullaniciadi).SingleOrDefault();
            if (login.kullaniciadi==Uye.kullaniciadi && login.email==Uye.email &&login.sifre==Uye.sifre)
            {
                Session["UyeId"] = login.uyeid;
                Session["KullaniciAdi"] = login.kullaniciadi;
                Session["YetkiId"] = login.yetkiid;
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View();

            }
        }

        public ActionResult Logout()

        {
            Session["UyeId"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Create ()

        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(uye Uye,HttpPostedFileBase foto)

        {

            if (ModelState.IsValid)
            {
                if (foto!=null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/uyefoto/" + newfoto);
                    Uye.foto = "/Uploads/uyefoto/" + newfoto;
                    Uye.yetkiid = 2;
                    db.uyes.Add(Uye);
                    db.SaveChanges();
                    Session["UyeId"] = Uye.uyeid;
                    Session["KullaniciAdi"] = Uye.kullaniciadi;
                    return RedirectToAction("Index", "Home");

                }

                else
                {
                    ModelState.AddModelError("Fotoğraf","Fotoğraf Seçiniz");
                }
            }
            return View(Uye);

        }

        public ActionResult Edit(int id)
        {
            var uye = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"]) != uye.uyeid)
            {
                return HttpNotFound();
            }


            return View(uye);
        }
        [HttpPost]
        public ActionResult Edit(uye Uye, int id, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                var uye = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();

                if (foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(uye.foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uye.foto));
                    }

                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/uyefoto/" + newfoto);
                    uye.foto = "/Uploads/uyefoto/" + newfoto;
                }
                uye.adsoyad = Uye.adsoyad;
                uye.kullaniciadi = Uye.kullaniciadi;
                uye.sifre = Uye.sifre;
                uye.email = Uye.email;
                db.SaveChanges();
                Session["kullaniciadi"] = uye.kullaniciadi;
                return RedirectToAction("Index", "Home", new { id = uye.uyeid });


            }
            return View();
        }

        public ActionResult UyeProfil(int id)
        {
            var Uye=db.uyes.Where(u => u.uyeid == id).SingleOrDefault();
            return View(Uye);
        }

    }
}