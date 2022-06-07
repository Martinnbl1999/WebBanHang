using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using PagedList;
using PagedList.Mvc;


namespace WebsiteBanHang.Controllers
{
    public class TrangChuController : Controller
    {
        DBWebsiteQuanLyCuaHangSoi data = new DBWebsiteQuanLyCuaHangSoi();
        // GET: TrangChu

        private List<SanPham> LaySanPhamMoi(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.ngaycapnhat).Take(count).ToList();
        }

        public ActionResult TrangChuCuaHang()
        {
            var sanpham = LaySanPhamMoi(3);
            return View(sanpham);
        }
        public ActionResult SanPhamMoi()
        {
            var sanphammoi = LaySanPhamMoi(5);
            return PartialView(sanphammoi);
        }
        public ActionResult LoaiSanPham()
        {
            var loaisanpham = from lsp in data.LoaiSanPhams select lsp;
                return PartialView(loaisanpham);
        }
        
        public ActionResult SPTheoLoai(int id)
        {
            var sptheoloai = from s in data.SanPhams where s.maloaisanpham == id select s;
            return View(sptheoloai);
        }
        public ActionResult TrangSanPham(int ? page)
        {
            // tao bien quy dinh so san pham tren moi trang
            int pageSize = 6;
            // tao bien so trang
            int pageNum = (page ?? 1);

            //lay san pham theo abum ban chay

            var sanpham = LaySanPhamMoi(15);
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }

        //public ActionResult TrangSanPham(int id)
        //{
        //    var sanpham1 = from s in data.SanPhams where s.maloaisanpham == id select s;
        //    return View(sanpham1);
        //}

        public ActionResult TrangChiTietSanPham(int id)
        {
            var sanpham = from s in data.SanPhams where s.masp == id select s;

            return View(sanpham.Single());
        }

        public ActionResult TrangChuTest()
        {
            return View();
        }

        public ActionResult Search(String TimKiem = "")
        {
            var list = data.SanPhams.Where(x => x.tensanpham.Contains(TimKiem)).ToList();
            return View(list);
        }

        //Get
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "makh,tenkh,diachi,email,sdt,username,password")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                data.KhachHangs.Add(khachHang);
                data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }
    }
}