using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.KH.Controllers
{
    public class ChiTiet_DonHang_SanphamController : Controller
    {
        private DBWebsiteQuanLyCuaHangSoi db = new DBWebsiteQuanLyCuaHangSoi();

        // GET: KH/ChiTiet_DonHang_Sanpham
        public ActionResult Index()
        {
            var chiTiet_DonHang_Sanpham = db.ChiTiet_DonHang_Sanpham.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(chiTiet_DonHang_Sanpham.ToList());
        }
        public ActionResult Index2()
        {
            var chiTiet_DonHang_Sanpham = db.ChiTiet_DonHang_Sanpham.Include(c => c.DonHang).Include(c => c.SanPham);
            
            return View(chiTiet_DonHang_Sanpham.ToList());
        }
        // GET: KH/ChiTiet_DonHang_Sanpham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTiet_DonHang_Sanpham chiTiet_DonHang_Sanpham = db.ChiTiet_DonHang_Sanpham.Find(id);
            if (chiTiet_DonHang_Sanpham == null)
            {
                return HttpNotFound();
            }
            return View(chiTiet_DonHang_Sanpham);
        }

        // GET: KH/ChiTiet_DonHang_Sanpham/Create
        public ActionResult Create()
        {
            ViewBag.madonhang = new SelectList(db.DonHangs, "madonhang", "madonhang");
            ViewBag.masp = new SelectList(db.SanPhams, "masp", "tensanpham");
            return View();
        }

        // POST: KH/ChiTiet_DonHang_Sanpham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madonhang,masp,soluong,dongia")] ChiTiet_DonHang_Sanpham chiTiet_DonHang_Sanpham)
        {
            if (ModelState.IsValid)
            {
                db.ChiTiet_DonHang_Sanpham.Add(chiTiet_DonHang_Sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.madonhang = new SelectList(db.DonHangs, "madonhang", "madonhang", chiTiet_DonHang_Sanpham.madonhang);
            ViewBag.masp = new SelectList(db.SanPhams, "masp", "tensanpham", chiTiet_DonHang_Sanpham.masp);
            return View(chiTiet_DonHang_Sanpham);
        }

        // GET: KH/ChiTiet_DonHang_Sanpham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTiet_DonHang_Sanpham chiTiet_DonHang_Sanpham = db.ChiTiet_DonHang_Sanpham.Find(id);
            if (chiTiet_DonHang_Sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.madonhang = new SelectList(db.DonHangs, "madonhang", "madonhang", chiTiet_DonHang_Sanpham.madonhang);
            ViewBag.masp = new SelectList(db.SanPhams, "masp", "tensanpham", chiTiet_DonHang_Sanpham.masp);
            return View(chiTiet_DonHang_Sanpham);
        }

        // POST: KH/ChiTiet_DonHang_Sanpham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madonhang,masp,soluong,dongia")] ChiTiet_DonHang_Sanpham chiTiet_DonHang_Sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTiet_DonHang_Sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.madonhang = new SelectList(db.DonHangs, "madonhang", "madonhang", chiTiet_DonHang_Sanpham.madonhang);
            ViewBag.masp = new SelectList(db.SanPhams, "masp", "tensanpham", chiTiet_DonHang_Sanpham.masp);
            return View(chiTiet_DonHang_Sanpham);
        }

        // GET: KH/ChiTiet_DonHang_Sanpham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTiet_DonHang_Sanpham chiTiet_DonHang_Sanpham = db.ChiTiet_DonHang_Sanpham.Find(id);
            if (chiTiet_DonHang_Sanpham == null)
            {
                return HttpNotFound();
            }
            return View(chiTiet_DonHang_Sanpham);
        }

        // POST: KH/ChiTiet_DonHang_Sanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTiet_DonHang_Sanpham chiTiet_DonHang_Sanpham = db.ChiTiet_DonHang_Sanpham.Find(id);
            db.ChiTiet_DonHang_Sanpham.Remove(chiTiet_DonHang_Sanpham);
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
