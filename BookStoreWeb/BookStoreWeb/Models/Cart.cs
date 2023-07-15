using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWeb.Models
{
    public class Cart
    {
        //tạo đối tượng data chứa dữ liệu từ model dbQLbanSach
        dbQLBanSachDataContext data = new dbQLBanSachDataContext();

        public int iMaSach { set; get; }
        public string sTenSach { set; get; }
        public string sAnhBia { set; get; }
        public Double dDonGia { set; get; }
        public int iSoLuong { set; get; }
        public Double dThanhTien 
        {
            get { return iSoLuong * dDonGia; }
        }
        // khởi tạo giỏ hàng theo mã sách được truyền vào với số lượng mặc đinh là 1
        public Cart (int MaSach)
        {
            iMaSach = MaSach;
            Sach book  = data.Saches.Single(n => n.MaSach == iMaSach);
            sTenSach = book.TenSach;
            sAnhBia = book.AnhBia;
            dDonGia = double.Parse(book.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}