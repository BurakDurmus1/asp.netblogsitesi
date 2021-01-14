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
    public class AdminYorumController : Controller
    {
        private geziDB db = new geziDB();

        // GET: AdminYorum
        public ActionResult Index()
        {
            var yorums = db.yorums.Include(y => y.uye).Include(y => y.yer);
            return View(yorums.ToList());
        }

        // GET: AdminYorum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: AdminYorum/Create
        public ActionResult Create()
        {
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi");
            ViewBag.yerid = new SelectList(db.yers, "yerid", "baslik");
            return View();
        }

        // POST: AdminYorum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yorumid,icerik,uyeid,yerid,tarih")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.yorums.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi", yorum.uyeid);
            ViewBag.yerid = new SelectList(db.yers, "yerid", "baslik", yorum.yerid);
            return View(yorum);
        }

        // GET: AdminYorum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi", yorum.uyeid);
            ViewBag.yerid = new SelectList(db.yers, "yerid", "baslik", yorum.yerid);
            return View(yorum);
        }

        // POST: AdminYorum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yorumid,icerik,uyeid,yerid,tarih")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi", yorum.uyeid);
            ViewBag.yerid = new SelectList(db.yers, "yerid", "baslik", yorum.yerid);
            return View(yorum);
        }

        // GET: AdminYorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: AdminYorum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yorum yorum = db.yorums.Find(id);
            db.yorums.Remove(yorum);
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
