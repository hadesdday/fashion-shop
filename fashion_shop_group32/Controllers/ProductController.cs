using fashion_shop_group32.Context;
using fashion_shop_group32.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
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



        public ActionResult ProductDetails(string id, string name)
        {

            ViewModelIndex2 viewModel = new ViewModelIndex2();
            Product p = new MockProduct().GetProductsByID(id);
            viewModel.product = p;
            viewModel.list1 = new MockProduct().GetColorsByIDProduct(id);
            viewModel.list2 = new MockProduct().GetSizesByIDProduct(id);
            viewModel.list3 = new MockProduct().GetRelatedProducts(p.ma_loaisp, p.loai);

            int commentQuantity = new MockProduct().GetReviewsCount(id);
            IEnumerable<Review> fourFirstComments = new MockProduct().GetReviews(id, 0, 5);
            viewModel.fourFirstComments = fourFirstComments;
            viewModel.commentQuantity = commentQuantity;
            if (commentQuantity > 4)
            {
                IEnumerable<Review> remainComments = new MockProduct().GetReviews(id, 5, commentQuantity);
                viewModel.isRemainingComment = true;
                viewModel.remainComments = remainComments;
            }
            else
            {
                viewModel.isRemainingComment = false;
            }
            return View(viewModel);
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
        public JsonResult PostComment(Review review)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    context.review.Add(review);
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return new JsonHttpStatusResult("Gửi đánh giá thất bại !", HttpStatusCode.Conflict);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new JsonHttpStatusResult("Vui lòng kiểm tra lại nội dung đánh giá của bạn !", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("Đánh giá của bạn đã được gửi đi !", HttpStatusCode.OK);
        }
    }
}