using fashion_shop_group32.Context;
using fashion_shop_group32.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers.admin
{
    public class ProductDetailsManagementController : Controller
    {
        public JsonResult Add(ProductDetailsEntity d)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var details = new ProductDetailsEntity
                    {
                        id_sanpham = d.id_sanpham,
                        ma_mau = d.ma_mau,
                        ma_size = d.ma_size,
                        id_anh = d.id_anh
                    };
                    context.chitietsanpham.Add(details);
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

        public JsonResult GetProductDetailsList()
        {
            List<ProductDetailsEntity> productDetailsList = new List<ProductDetailsEntity>();

            using (var ctx = new AdminDbContext())
            {
                productDetailsList = ctx.chitietsanpham.ToList<ProductDetailsEntity>();
            }

            if (productDetailsList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = productDetailsList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetProductDetails(int id)
        {
            ProductDetailsEntity details = null;
            using (var ctx = new AdminDbContext())
            {
                details = ctx.chitietsanpham.Where(t => (t.id == id)).FirstOrDefault<ProductDetailsEntity>();
            }

            if (details == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(ProductDetailsEntity p)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.chitietsanpham.Where(t => (t.id == p.id)).FirstOrDefault<ProductDetailsEntity>();
                if (current != null)
                {
                    current.ma_mau = p.ma_mau;
                    current.ma_size = p.ma_size;
                    current.id_anh = p.id_anh;
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
                var current = ctx.chitietsanpham.Where(t => (t.id == id)).FirstOrDefault<ProductDetailsEntity>();
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