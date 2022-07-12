﻿using fashion_shop_group32.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public ActionResult ProductList(string cat, string loai, string mau, string size, string gia, string keyword, string page)
        {
            int numpage;
            if (page == "" || page == null)
                numpage = 1;
            else numpage = Int32.Parse(page);
            ViewBag.loai = loai;
            ViewBag.cat = cat;
            ViewBag.mau = mau;
            ViewBag.size = size;
            ViewBag.gia = gia;
            ViewBag.keyword = keyword;
            ViewBag.page = numpage;
            System.Diagnostics.Debug.WriteLine("ProductList3.");
            var model = _product.GetProductsByCategoryAndLoaiAndFilter(cat, loai, mau, size, gia, keyword, numpage);
            ViewModelIndex3 viewModel = new ViewModelIndex3();
            viewModel.count = new MockProduct().NumberProductinList(cat, loai, mau, size, gia, keyword);
            viewModel.list1 = model;
            System.Diagnostics.Debug.WriteLine(viewModel.count);
            ViewBag.numpage = viewModel.count;
            return View(viewModel);
        }

        //[ActionName("ProductList1")]
        //public ActionResult ProductList(string cat)
        //{
        //    ViewBag.cat = cat;
        //    System.Diagnostics.Debug.WriteLine("ProductList1.");
        //    var model = _product.GetProductsByCategory(cat);
        //    return View(model);
        //}
        [ActionName("ProductList1")]
        public ActionResult ProductList(string keyword)
        {
            ViewBag.keyword = keyword;
            System.Diagnostics.Debug.WriteLine("ProductList1.");
            var model = _product.GetProductsBySearch(keyword);
            return View(model);
        }


        public ActionResult ProductDetails(string id, string name)
        {
            ViewModelIndex2 viewModel = new ViewModelIndex2();
            Product p = new MockProduct().GetProductsByID(id);
            viewModel.product = p;
            viewModel.list1 = new MockProduct().GetColorsByIDProduct(id);
            viewModel.list2 = new MockProduct().GetSizesByIDProduct(id);
            viewModel.list3 = new MockProduct().GetRelatedProducts(p.ma_loaisp, p.loai);
            return View(viewModel);
            //return View("ProductDetails");
        }
        public JsonResult SetViewBagColor(string color)
        {
            ViewBag.color = color;

            return Json(new { Result = color }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetViewBagSize(string size)
        {
            ViewBag.size = size;
            return Json(new { Result = size }, JsonRequestBehavior.AllowGet);
        }
    }
}