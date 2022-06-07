using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebsiteBanHang.Models;


namespace WebsiteBanHang.Areas.DangNhap.Controllers
{
    public class AccountController : Controller
    {
        DBWebsiteQuanLyCuaHangSoi data = new DBWebsiteQuanLyCuaHangSoi();
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: DangNhap/Account
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangNhapAdNv()
        {
            return View();
        }

        public ActionResult logout()
        {
            Session["khUser"] = null;
            return RedirectToAction("TrangChuCuaHang", "TrangChu", new { area = "" });
        }

        void connectionString()
        {
            con.ConnectionString = "Data Source=DESKTOP-NV6R6OR;Initial Catalog=WebsiteQuanLyCuaHangSoi;Integrated Security=True";
        }
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
                return RedirectToAction("TrangChuCuaHang","TrangChu", new { area = "" });
            }

            return View(khachHang);
        }

        [HttpPost]
        //public ActionResult Veritify(Account acc)
        //{
        //    // cach 1

        //    connectionString();
        //    con.Open();
        //    com.Connection = con;
        //    com.CommandText = "select * from NhanVien where username='" + acc.Name + "' and password='" + acc.Password + "'";
        //    dr = com.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        con.Close();
        //        //return RedirectToAction("Index", "BookStore");
        //        return View("Yes");
        //    }
        //    else
        //    {
        //        con.Close();
        //        return View("No");
        //    }

        //    //
           
        //}
        
        public ActionResult Veritify2(FormCollection f)
        {
            string usr = f["Name"].ToString();

            string pas = f["Password"].ToString();
            //int pas = int.Parse(f["Password"].ToString());

            List<NhanVien> kq = (from x in data.NhanViens where x.username == usr && x.password == pas select x).ToList<NhanVien>();
            if (kq.Count > 0)
            {
                int role = ((kq[0].role == null || kq[0].role == 0) ? 0 : 1);

                Session["aUser"] = kq[0].username;
                Session["aRole"] = role;
                if (role == 0)
                {
                    return RedirectToAction("Index", "NV/TrangNhanVien", new { area = "" });
                }
                else
                    return RedirectToAction("Index", "NV/NhanViens", new { area = "" });

            }
            else
            {
                ViewBag.error = "Sai tài khoản hoặc mật khẩu";
                //return RedirectToAction("IndexLogin", "DangNhapLogin");
                return View("DangNhap");
            }
        }

        public ActionResult Veritify(FormCollection f)
        {
            string usr = f["Name"].ToString();
            string pas = f["Password"].ToString();

            List<KhachHang> kq = (from x in data.KhachHangs where x.username == usr && x.password == pas select x).ToList<KhachHang>();

            if (kq.Count > 0)
            {

                Session["khUser"] = usr;
                Session["Khachhang"] = kq[0];
                return RedirectToAction("TrangChuCuaHang", "TrangChu", new { area = "" });
                //return RedirectToAction("IndexRoleUser", "TTKhachHangs");
                //return RedirectToAction("Index2", "Account");
            }

            return View("DangNhap");
        }
    }
    
   
}