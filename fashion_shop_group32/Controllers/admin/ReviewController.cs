using fashion_shop_group32.Context;
using fashion_shop_group32.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers.admin
{
    public class ReviewController : Controller
    {
        public JsonResult Add(Review r)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var review = new Review
                    {
                        id_sanpham = r.id_sanpham,
                        username = r.username,
                        sosao = r.sosao,
                        noidung = r.noidung
                    };
                    context.review.Add(review);
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return new JsonHttpStatusResult("existed data", HttpStatusCode.Conflict);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new JsonHttpStatusResult("invalid data", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("success", HttpStatusCode.OK);
        }

        public JsonResult GetReviewList()
        {
            List<Review> reviewList = new List<Review>();

            using (var ctx = new AdminDbContext())
            {
                reviewList = ctx.review.ToList<Review>();
            }

            if (reviewList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = reviewList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetReview(int id)
        {
            Review s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.review.Where(t => t.id == id).FirstOrDefault<Review>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(Review t)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.review.Where(z => t.id == z.id).FirstOrDefault<Review>();
                if (current != null)
                {
                    current.sosao = t.sosao;
                    current.noidung = t.noidung;
                    ctx.SaveChanges();
                }
                else
                {
                    return new JsonHttpStatusResult("update error", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("success update", HttpStatusCode.OK);
        }

        public JsonResult Delete(int id)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.review.Where(t => t.id == id).FirstOrDefault<Review>();
                if (current != null)
                {
                    ctx.Entry(current).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
                else
                {
                    return new JsonHttpStatusResult("delete error", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("delete success", HttpStatusCode.OK);
        }
    }
}