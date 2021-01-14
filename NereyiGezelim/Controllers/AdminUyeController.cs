using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NereyiGezelim.Models;

namespace NereyiGezelim.Controllers
{
    public class AdminUyeController : Controller
    {
        private geziDB db = new geziDB();

        // GET: AdminUye
        public ActionResult Index()
        {
            var uyes = db.uyes.Include(u => u.yetki);
            return View(uyes.ToList());
        }

        // GET: AdminUye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uye uye = db.uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: AdminUye/Create
        public ActionResult Create()
        {
            ViewBag.yetkiid = new SelectList(db.yetkis, "yetkiid", "yetki1");
            return View();
        }

        // POST: AdminUye/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "uyeid,kullaniciadi,email,sifre,adsoyad,foto,yetkiid")] uye uye)
        {
            if (ModelState.IsValid)
            {
                db.uyes.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.yetkiid = new SelectList(db.yetkis, "yetkiid", "yetki1", uye.yetkiid);
            return View(uye);
        }

        // GET: AdminUye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uye uye = db.uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            ViewBag.yetkiid = new SelectList(db.yetkis, "yetkiid", "yetki1", uye.yetkiid);
            return View(uye);
        }

        // POST: AdminUye/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uyeid,kullaniciadi,email,sifre,adsoyad,foto,yetkiid")] uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.yetkiid = new SelectList(db.yetkis, "yetkiid", "yetki1", uye.yetkiid);
            return View(uye);
        }

        // GET: AdminUye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uye uye = db.uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: AdminUye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uye uye = db.uyes.Find(id);
            db.uyes.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
