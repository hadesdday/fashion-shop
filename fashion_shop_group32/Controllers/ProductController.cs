using fashion_shop_group32.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        public ProductController()
        {
            _product = new MockProduct();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View("ProductList");
        }
        [ActionName("ProductList3")]
        public ActionResult ProductList(string cat, string loai)
        {
            ViewBag.loai = loai;
            ViewBag.cat = cat;
            ViewBag.filter = "";
            Console.WriteLine("ProductList2.");
            var model = _product.GetProductsByCategoryAndLoai(cat, loai);
            return View(model);
        }

        [ActionName("ProductList")]
        public ActionResult ProductList(string cat, string loai,string mau, string size, string gia)
        {
            ViewBag.loai = loai;
            ViewBag.cat = cat;
            ViewBag.mau = mau;
            ViewBag.size = size;
            ViewBag.gia = gia;
            System.Diagnostics.Debug.WriteLine("ProductList3.");
            var model = _product.GetProductsByCategoryAndLoaiAndFilter(cat,loai,mau,size,gia);
            return View(model);
        }

        [ActionName("ProductList1")]
        public ActionResult ProductList(string cat)
        {
            ViewBag.cat = cat;
            System.Diagnostics.Debug.WriteLine("ProductList1.");
            var model = _product.GetProductsByCategory(cat);
            return View(model);
        }


        
        public ActionResult ProductDetails(string id,string name)
        {
            ViewModelIndex2 viewModel = new ViewModelIndex2();
            viewModel.product = new MockProduct().GetProductsByID(id);
            viewModel.list1 = new MockProduct().GetColorsByNameProduct(name);
            viewModel.list2 = new MockProduct().GetSizesByNameProduct(name);
            return View(viewModel);
            //return View("ProductDetails");
        }
    }
}