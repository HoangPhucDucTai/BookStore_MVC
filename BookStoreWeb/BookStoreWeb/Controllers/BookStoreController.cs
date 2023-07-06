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
        // Sản phẩm sách theo chủ đề
        public ActionResult TopicName(int id)
        {
            var book = from b in data.Saches where b.MaChuDe == id select b;
            return View(book);
        }
        //sản phẩm sách theo nhà xuất bản
        public ActionResult PublisherName(int id)
        {
            var book = from b in data.Saches where b.MaNXB == id select b;
            return View(book);
        }
        //chi tiết sản phẩm (sách)
        public ActionResult Details(int id)
        {
            var book = from b in data.Saches where b.MaSach == id select b;
            return View(book.Single());
        }

    }
}