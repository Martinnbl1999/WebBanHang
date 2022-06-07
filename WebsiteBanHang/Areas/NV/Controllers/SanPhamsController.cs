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
    public class SanPhamsController : Controller
    {
        private DBWebsiteQuanLyCuaHangSoi db = new DBWebsiteQuanLyCuaHangSoi();

        // GET: NV/SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.QuocGia);
            return View(sanPhams.ToList());
        }

        // GET: NV/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: NV/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.maloaisanpham = new SelectList(db.LoaiSanPhams, "maloaisanpham", "tenloaisanpham");
            ViewBag.maquocgia = new SelectList(db.QuocGias, "maquocgia", "quogia");
            return View();
        }

        // POST: NV/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masp,maloaisanpham,tensanpham,mota,hinhanh,maquocgia,ngaycapnhat,soluong,dongia")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maloaisanpham = new SelectList(db.LoaiSanPhams, "maloaisanpham", "tenloaisanpham", sanPham.maloaisanpham);
            ViewBag.maquocgia = new SelectList(db.QuocGias, "maquocgia", "quogia", sanPham.maquocgia);
            return View(sanPham);
        }

        // GET: NV/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.maloaisanpham = new SelectList(db.LoaiSanPhams, "maloaisanpham", "tenloaisanpham", sanPham.maloaisanpham);
            ViewBag.maquocgia = new SelectList(db.QuocGias, "maquocgia", "quogia", sanPham.maquocgia);
            return View(sanPham);
        }

        // POST: NV/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "masp,maloaisanpham,tensanpham,mota,hinhanh,maquocgia,ngaycapnhat,soluong,dongia")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maloaisanpham = new SelectList(db.LoaiSanPhams, "maloaisanpham", "tenloaisanpham", sanPham.maloaisanpham);
            ViewBag.maquocgia = new SelectList(db.QuocGias, "maquocgia", "quogia", sanPham.maquocgia);
            return View(sanPham);
        }

        // GET: NV/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: NV/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
