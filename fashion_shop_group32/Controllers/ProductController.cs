using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View("ProductList");
        }

        public ActionResult ProductDetails()
        {
            return View("ProductDetails");
        }
    }
}