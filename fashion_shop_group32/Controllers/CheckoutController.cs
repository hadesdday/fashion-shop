using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fashion_shop_group32.Models.Dao;
namespace fashion_shop_group32.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View("CheckoutHome");
        }
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment( string pttt,string ten_kh, string diachi, string sodt, string email)
        {
            List<CartDao> carts = (List<CartDao>)Session["cart"];
            string mgg ;
            if (Session["coupon"] != null)
            {
                mgg = (string)Session["coupon"];
            }
            else
            {
                mgg = "ma01";
            }
            string paymentMt = "";
            PaymentMethod paymentMethod = checkPaymentMethod(pttt);
            if (paymentMethod != null)
            {
                paymentMt = paymentMethod.paymentMethodCode;
            }
            
            if (doPayment(mgg, paymentMt, getMoneyOfCart(), ten_kh, diachi, sodt, email, carts))
            {
                ViewBag.Message = "sucess register payment";
                return RedirectToAction("ClearOrder");
            }
            else
            {
                ViewBag.Message = "payment fail please do again";
                return View();
            }
        }
        public ActionResult Coupon(string couponId)
        {
            if (checkCoupon(couponId) != null)
            {
                Session["coupon"] = couponId;
                ViewBag.CouponMessage = "suscess aplly coupon";
            }
            else
            {
                Session["coupon"] = couponId;
                ViewBag.CouponMessage = "coupon does exit";
            }
            return View("CheckoutHome");
        }
        public double getMoneyOfCart()
        {
            List<CartDao> carts = (List<CartDao>)Session["cart"];
            double money = 0;
            foreach (CartDao item in (List<CartDao>)Session["cart"])
            {
                money = money + item.Pro.getTotalMoney();
            }
            return money;
        }
        public PaymentMethod checkPaymentMethod(string paymentMethod)
        {
            List<PaymentMethod> payments = Models.Dao.CheckOutDao.GetPaymentMethods();
            PaymentMethod result = null;
            foreach(PaymentMethod item in payments)
            {
                if (paymentMethod.Equals(item.paymentMethodCode)){
                    result = item;
                }
            }
            return result;
        }
        public ActionResult ClearOrder()
        {
            List<CartDao> carts = (List<CartDao>)Session["cart"];
            carts.Clear();
            return Redirect("/Home/Index");
        }

        public Boolean doPayment(string mgg,string pttt,double trigia,string ten_kh,string diachi,string sodt,string email,List<CartDao> cart)
        {
            if (Session["Idkh"] == null)
            {
                return Models.Dao.CheckOutDao.SaveBillNotUSer(mgg, pttt, trigia, ten_kh, diachi, sodt, email, cart);
            }
            else {
                return Models.Dao.CheckOutDao.SaveBillUser(mgg, pttt, trigia,Session["Idkh"].ToString() , cart);
            }
        }
        public string checkCoupon(string couponId)
        {
            return Models.Dao.CheckOutDao.getCoupon(couponId);
        }
    }
}