using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NereyiGezelim.Models;
using System.Web.Helpers;
using System.IO;

namespace NereyiGezelim.Controllers 
{
    public class AdminYerController : Controller
    {
        geziDB db = new geziDB();
        // GET: AdminYer
        public ActionResult Index()
        {
            var yer = db.yers.ToList();
            return View(yer);
        }

        // GET: AdminYer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminYer/Create
        public ActionResult Create()
        {
            ViewBag.kategoriid = new SelectList(db.kategoris, "kategoriid", "kategoriadi");
            return View();
        }

        // POST: AdminYer/Create
        [HttpPost]
        public ActionResult Create(yer Yer,string etiketler,HttpPostedFileBase foto)
        {
            try
            {
                if (foto != null)
                {

                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/yerfoto/" + newfoto);
                    Yer.foto = "/Uploads/yerfoto/" + newfoto ;
                    
                   
                }

                if(etiketler!=null)
                {
                    string[] etiketdizi = etiketler.Split(',');
                    foreach (var i in etiketdizi) 

                    {
                        var yenietiket = new etiket { etiketadi = i };
                        db.etikets.Add(yenietiket);
                        Yer.etikets.Add(yenietiket);
                    }
                }
                Yer.uyeid =Convert.ToInt32( Session["UyeId"]);
                db.yers.Add(Yer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(Yer);
            }
        }

        // GET: AdminYer/Edit/5
        public ActionResult Edit(int id)
        {
            var Yer = db.yers.Where(m => m.yerid == id).SingleOrDefault();
            if(Yer==null)
            {
                return HttpNotFound();
            }
            ViewBag.kategoriid = new SelectList(db.kategoris, "kategoriid", "katagoriadi",Yer.kategoriid);
            return View(Yer);
        }

        // POST: AdminYer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase foto,yer Yer)
        {
            try
            {
                // TODO: Add update logic here
                var Yers = db.yers.Where(m => m.yerid == id).SingleOrDefault();
                if (foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Yers.foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(Yers.foto));
                    }
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/yerfoto/" + newfoto);
                    Yers.foto = "/Uploads/yerfoto/" + newfoto;
                    Yers.baslik = Yer.baslik;
                    Yers.icerik = Yer.icerik;
                    Yers.kategoriid = Yer.kategoriid;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                

                return View();
            }
            catch
            {
                ViewBag.kategoriid = new SelectList(db.kategoris, "kategoriid", "kategoriadi", Yer.kategoriid);
                return View(Yer);
            }
        }

        // GET: AdminYer/Delete/5
        public ActionResult Delete(int id)
        {
            var Yer = db.yers.Where(m => m.yerid == id).SingleOrDefault();
            if(Yer==null)
            {
                return HttpNotFound();
            }
            return View(Yer);
        }

        // POST: AdminYer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var Yers = db.yers.Where(m => m.yerid == id).SingleOrDefault();

                if (Yers == null)
                {
                    return HttpNotFound();
                }

                if (System.IO.File.Exists(Server.MapPath(Yers.foto)))
                {
                    System.IO.File.Delete(Server.MapPath(Yers.foto));
                }


                foreach (var i in Yers.yorums.ToList())

                {

                    db.yorums.Remove(i);
                }

                foreach (var i in Yers.etikets.ToList())

                {
                    db.etikets.Remove(i);
                }

                db.yers.Remove(Yers);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
