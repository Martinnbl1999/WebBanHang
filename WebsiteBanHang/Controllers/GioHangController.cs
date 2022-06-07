using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
namespace WebsiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        DBWebsiteQuanLyCuaHangSoi data = new DBWebsiteQuanLyCuaHangSoi();
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("TrangChuCuaHang", "TrangChu");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        // GET: GioHang
        //Lấy giỏ hàng
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if(lstGiohang == null)
            {
                // Nếu giỏ hàng rỗng thì khởi tạo lại list
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }


        // Thêm giỏ hàng
        public ActionResult ThemGioHang(int imasp, string strURL)
        {
            //Lay ra Session gio hang
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra san phảm này có tồn tại trong session gio hang ko
            Giohang sanpham = lstGiohang.Find(n => n.imasp == imasp);
            if(sanpham == null)
            {
                sanpham = new Giohang(imasp);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strURL);
            }
        }

        // Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMasp)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.imasp == iMasp);

            if(sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.imasp == iMasp);
                return RedirectToAction("GioHang");
            }
            if(lstGiohang.Count == 0)
            {
                return RedirectToAction("TrangChuCuaHang", "TrangChu");
            }
            return RedirectToAction("GioHang");
        }

        //Cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int iMasp, FormCollection f)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.imasp == iMasp);

            if(sanpham != null)
            {
                sanpham.isoluong = int.Parse(f["numSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        // Xóa giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("TrangChuCuaHang","TrangChu");
        }
        // Cập nhật số lượng
        private int TongSoLuong()
        {
            int itongsoluong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;//
            if (lstGiohang != null)
            {
                itongsoluong = lstGiohang.Sum(n => n.isoluong);
            }
            return itongsoluong;
        }

        // Cập nhật số tiền
        private double TongTien()
        {
            double itongtien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;//
            if(lstGiohang != null)
            {
                itongtien = lstGiohang.Sum(n => n.iThanhTien);
            }
            return itongtien;
        }


       

        //Tạo Partial view de hien thị thông tin gio hàng
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        [HttpGet]
        //Hien thi view dathang de cap nhat thong tin cho don hang
        public ActionResult DatHang()
        {
            if(Session["khUser"] == null || Session["khUser"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap/Account");
            }
            if(Session["Giohang"] != null)
            {
                //Lay gio hang
                List<Giohang> lstGiohang = Laygiohang();
                ViewBag.Tongsoluong = TongSoLuong();
                ViewBag.Tongtien = TongTien();
                return View(lstGiohang);
                //return RedirectToAction("TrangChuCuaHang", "TrangChu");
            }

            //Lay gio hang
            //List<Giohang> lstGiohang = Laygiohang();
            //ViewBag.Tongsoluong = TongSoLuong();
            //ViewBag.Tongtien = TongTien();
            //return View(lstGiohang);

            return RedirectToAction("TrangChuCuaHang", "TrangChu");
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DatHang(FormCollection collection)
        {
            // Them Don Hang
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["Khachhang"];
            List<Giohang> gh = Laygiohang();
            dh.makh = kh.makh;
            dh.ngaydathang = DateTime.Now;
            var ngaygiaohang = String.Format("{0:MM/dd/yyyy}", collection["ngaygiaohang"]);
            dh.ngaygiaohang = DateTime.Parse(ngaygiaohang);
            data.DonHangs.Add(dh);
            data.SaveChanges();
            foreach (var item in gh)
            {
                ChiTiet_DonHang_Sanpham ctdh = new ChiTiet_DonHang_Sanpham();
                ctdh.madonhang = dh.madonhang;
                ctdh.masp = item.imasp;
                ctdh.soluong = item.isoluong;
                ctdh.dongia = (decimal)item.idongia;
                data.ChiTiet_DonHang_Sanpham.Add(ctdh);

            }
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "GioHang");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }

    }
}