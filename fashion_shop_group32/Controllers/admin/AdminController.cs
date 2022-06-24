using fashion_shop_group32.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
namespace fashion_shop_group32.Controllers.admin
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View("OrderManagement");
        }
        public ActionResult Product()
        {
            return View("ProductManagement");
        }
        public ActionResult UserManagement()
        {
            return View("UserManagement");
        }
        public ActionResult Customer()
        {
            return View("CustomerManagement");
        }
        public ActionResult Sales()
        {
            return View("SalesManagement");
        }
        public ActionResult Review()
        {
            return View("ReviewManagement");
        }
        public ActionResult PaymentMethod()
        {
            return View("PaymentMethodManagement");
        }
        public ActionResult SizeBoard()
        {
            return View("SizeBoardManagement");
        }
        public ActionResult ColorBoard()
        {
            return View("ColorBoardManagement");
        }
        public ActionResult ProductType()
        {
            return View("ProductTypeManagement");
        }
    }
}