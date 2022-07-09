using fashion_shop_group32.Models;
using fashion_shop_group32.Models.Dao;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace fashion_shop_group32.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            return RedirectToAction("ShowCart");
        }
        //phương thức check xem sản phẩm đã tồn tại trong giỏ hàng hay chưa
        //nếu đã tồn tại trả về index vị trí
        public int isExits(String id)
        {
            List<CartDao> items = (List<CartDao>)Session["cart"];
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Pro.id_sanpham.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        //phương thức thêm vào giỏ hàng sản phẩm
        public ActionResult AddToCart(string product_id)
        {
            MockProduct mockProduct = new MockProduct();
            Product product = mockProduct.GetProductsByID(product_id);
            /*
            if (product != null)
            {
                CartItem cart = (CartItem)Session["Cart"];
                if(cart == null)
                {
                    cart = CartItem.getInstance();
                }
                cart.Put(product);
            }
            */
            if (Session["cart"] == null)
            {
                List<CartDao> carts = new List<CartDao>();
                carts.Add(new CartDao(product_id, product));
                Session["cart"] = carts;
            }
            else
            {
                List<CartDao> carts = (List<CartDao>)Session["cart"];
                if (isExits(product_id) == -1)
                {
                    carts.Add(new CartDao(product_id, product));
                }
                else
                {
                    carts[isExits(product_id)].Pro.quantitySold += 1;
                }
                Session["cart"] = carts;
            }
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult ShowCart()
        {
            List<CartDao> carts = (List<CartDao>)Session["cart"];
            if (carts == null)
            {
                carts = new List<CartDao>();
            }
            Session["cart"] = carts;

            return View("CartHome");
        }
        public ActionResult DeleteCart(string product_id)
        {
            int index = isExits(product_id);
            List<CartDao> carts = (List<CartDao>)Session["cart"];
            carts.RemoveAt(index);
            Session["cart"] = carts;
            return View("CartHome");
        }
    }
}