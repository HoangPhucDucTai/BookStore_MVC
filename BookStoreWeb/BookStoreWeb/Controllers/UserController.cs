
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreWeb.Models;


namespace BookStoreWeb.Controllers
{
    public class UserController : Controller
    {
        dbQLBanSachDataContext db = new dbQLBanSachDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        // hàm register nhận dữ liệu từ layout Register và thực hiện việc tạo mới dữ liệu
        [HttpPost]
        public ActionResult Register (FormCollection collection, KhachHang kh)
        {
            // gán các giá trị người dùng nhập cho các biến
            var fullname = collection["HoTenKH"];
            var username = collection["TenDangNhap"];
            var password = collection["MatKhau"];
            var retypepassword = collection["NhapLaiMatKhau"];
            var address = collection["DiaChi"];
            var email = collection["Email"];
            var phone = collection["SoDienThoai"];
            var dateofbirth = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);

            if (String.IsNullOrEmpty(fullname))
            {
                ViewData["Loi1"] = "Không được để trống";
            }
            else if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(retypepassword))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Phải nhập email";
            }
            else if (String.IsNullOrEmpty(phone))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";
            }
            else
            {
                //gán địa chỉ cho đối tượng được tạo mới (kh)
                kh.HoTen = fullname;
                kh.TaiKhoan = username;
                kh.MatKhau = password;
                kh.DiaChi = address;
                kh.Email = email;
                kh.DienThoai = phone;
                kh.NgaySinh = DateTime.Parse(dateofbirth);
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();

                return RedirectToAction("Dangnhap");
            }
            return this.Register();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var username = collection["TenDangNhap"];
            var password = collection["MatKhau"];

            if (String.IsNullOrEmpty(password))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)

                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == username && n.MatKhau == password);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "BookStore");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}