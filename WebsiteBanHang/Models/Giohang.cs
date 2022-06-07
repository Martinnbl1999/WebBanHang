using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class Giohang
    {
        DBWebsiteQuanLyCuaHangSoi data = new DBWebsiteQuanLyCuaHangSoi();
        public int imasp { set; get; }
        public string itensanpham { set; get; }

        public string ihinhanh { set; get; }

        public Double idongia { set; get; }

        public int isoluong { set; get; }

        public Double iThanhTien
        {
            get { return isoluong * idongia; }
        }

        // Khởi tạo giỏ hàng theo sanpham duoc truyen vao voi soluong mac dinh la 1

        public Giohang(int masp)
        {
            imasp = masp;
            SanPham sanpham = data.SanPhams.Single(n => n.masp == imasp);
            itensanpham = sanpham.tensanpham;
            ihinhanh = sanpham.hinhanh;
            idongia = double.Parse(sanpham.dongia.ToString());
            isoluong = 1;

        }
    }
}