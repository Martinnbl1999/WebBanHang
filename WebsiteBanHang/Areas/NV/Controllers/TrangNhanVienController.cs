using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanHang.Areas.NV.Controllers
{
    public class TrangNhanVienController : Controller
    {
        DBWebsiteQuanLyCuaHangSoi data = new DBWebsiteQuanLyCuaHangSoi();
        // GET: NV/TrangNhanVien
        public ActionResult TrangNhanVien()
        {
            return View();
        }
        private List<SanPham> LaySanPhamMoi(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.ngaycapnhat).Take(count).ToList();
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
        public ActionResult TrangSanPham(int? page)
        {
            // tao bien quy dinh so san pham tren moi trang
            int pageSize = 6;
            // tao bien so trang
            int pageNum = (page ?? 1);

            //lay san pham theo abum ban chay

            var sanpham = LaySanPhamMoi(15);
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }
    }
}