using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.NV.Controllers
{
    public class QuocGiasController : Controller
    {
        private DBWebsiteQuanLyCuaHangSoi db = new DBWebsiteQuanLyCuaHangSoi();

        // GET: NV/QuocGias
        public ActionResult Index()
        {
            return View(db.QuocGias.ToList());
        }

        // GET: NV/QuocGias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuocGia quocGia = db.QuocGias.Find(id);
            if (quocGia == null)
            {
                return HttpNotFound();
            }
            return View(quocGia);
        }

        // GET: NV/QuocGias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NV/QuocGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maquocgia,quogia")] QuocGia quocGia)
        {
            if (ModelState.IsValid)
            {
                db.QuocGias.Add(quocGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quocGia);
        }

        // GET: NV/QuocGias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuocGia quocGia = db.QuocGias.Find(id);
            if (quocGia == null)
            {
                return HttpNotFound();
            }
            return View(quocGia);
        }

        // POST: NV/QuocGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maquocgia,quogia")] QuocGia quocGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quocGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quocGia);
        }

        // GET: NV/QuocGias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuocGia quocGia = db.QuocGias.Find(id);
            if (quocGia == null)
            {
                return HttpNotFound();
            }
            return View(quocGia);
        }

        // POST: NV/QuocGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuocGia quocGia = db.QuocGias.Find(id);
            db.QuocGias.Remove(quocGia);
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
