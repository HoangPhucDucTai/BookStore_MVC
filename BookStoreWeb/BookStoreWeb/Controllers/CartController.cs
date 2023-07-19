using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreWeb.Models;

namespace BookStoreWeb.Controllers
{
    public class CartController : Controller
    {
        dbQLBanSachDataContext data = new dbQLBanSachDataContext();
        // GET: Cart

        //lấy  ds giỏ hàng
        public List<Cart> TakeList()
        {
            List<Cart> lstCart = Session["GioHang"] as List<Cart>;
            // nếu giỏ hàng null thì khởi tạo
            if(lstCart == null)
            {
                lstCart = new List<Cart>();
                Session["GioHang"] = lstCart;
            }    
            return lstCart;
        }
        //add to cart
        public ActionResult AddToCart(int iMaSach, string strURL)
        {
            // lấy session giỏ hàng
            List<Cart> lstCart = TakeList();
            //check book trong session giỏ hàng
            Cart product = lstCart.Find(n => n.iMaSach == iMaSach);
            if (product == null)
            {
                product = new Cart(iMaSach);
                lstCart.Add(product);
                return Redirect(strURL);
            }
            else
            {
                product.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //tong so luong
        private int Quantity()
        {
            int iTongSoLuong = 0;
            List<Cart> lstCart = Session["GioHang"] as List<Cart>;
            if (lstCart != null)
            {
                iTongSoLuong = lstCart.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        // tong tien
        private double TotalFunds()
        {
            double iTongTien = 0;
            List<Cart> lstCart = Session["GioHang"] as List<Cart>;
            if (lstCart != null)
            {
                iTongTien = lstCart.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        // gio hang 
        public ActionResult Cart()
        {
            List<Cart> lstCart = TakeList();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.Quantity = Quantity();
            ViewBag.TotalFunds = TotalFunds();
            return View(lstCart);
        }
        //partial view cart
        public ActionResult CartPartial()
        {
            ViewBag.Quantity = Quantity();
            ViewBag.TotalFunds = TotalFunds();
            return PartialView();
        }
        //delete cart 
        public ActionResult DeleteCart(int iMaSanPham)
        {
            //lay cart tu session
            List<Cart> lstCart = TakeList();
            //check sach xem da co trong cart chua
            Cart product = lstCart.SingleOrDefault(n => n.iMaSach == iMaSanPham);
            if (product != null)
            {
                lstCart.RemoveAll(n => n.iMaSach == iMaSanPham);
                return RedirectToAction("Cart");
            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            return RedirectToAction("Cart");
        }
        //update cart
        public ActionResult UpdateCart(int iMaSanPham, FormCollection f)
        {
            //lay cart tu session
            List<Cart> lstCart = TakeList();
            //check sach xem da co trong cart chua
            Cart product = lstCart.SingleOrDefault(n => n.iMaSach == iMaSanPham);
            if (product != null)
            {
                product.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("Cart");
        }
    }
}