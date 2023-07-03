using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreWeb.Models;

namespace BookStoreWeb.Controllers
{
    public class BookStoreController : Controller
    {
        // tạo một đối tượng chứa toàn bộ csdl bán sách
        dbQLBanSachDataContext data = new dbQLBanSachDataContext();

        private List<Sach> Newbook(int count)
        {
            //sắp xếp sách giảm dần theo ngày cập nhật
            return data.Saches.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        // GET: BookStore
        public ActionResult Index()
        {
            //lấy 5 quyển sách mới nhất show lên trang chủ
            var newbook = Newbook(5);
            return View(newbook);
        }

        //Action chủ đề sách
        public ActionResult Topic()
        {
            var topic = from tp in data.ChuDes select tp;
            return PartialView(topic);
        }

        //Action nhà xuất bản
        public ActionResult Publisher()
        {
            var publisher = from pbs in data.NhaXuatBans select pbs;
            return PartialView(publisher);
        }
    }
}