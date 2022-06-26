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
    public class ProductTypeController : Controller
    {
        public JsonResult Add(ProductType t)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var type = new ProductType
                    {
                        ma_loaisp = t.ma_loaisp,
                        ten_loaisp = t.ten_loaisp
                    };
                    context.loaisanpham.Add(type);
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

        public JsonResult GetProductTypeList()
        {
            List<ProductType> typeList = new List<ProductType>();

            using (var ctx = new AdminDbContext())
            {
                typeList = ctx.loaisanpham.ToList<ProductType>();
            }

            if (typeList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = typeList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetProductType(string id)
        {
            ProductType s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.loaisanpham.Where(t => t.ma_loaisp.Equals(id)).FirstOrDefault<ProductType>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(ProductType t)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.loaisanpham.Where(z => z.ma_loaisp.Equals(t.ma_loaisp)).FirstOrDefault<ProductType>();
                if (current != null)
                {
                    current.ten_loaisp = t.ten_loaisp;
                    ctx.SaveChanges();
                }
                else
                {
                    return new JsonHttpStatusResult("update error", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("success update", HttpStatusCode.OK);
        }

        public JsonResult Delete(string id)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.loaisanpham.Where(z => z.ma_loaisp.Equals(id)).FirstOrDefault<ProductType>();
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