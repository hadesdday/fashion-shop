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
    public class SaleController : Controller
    {
        public JsonResult Add(Sale s)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var sale = new Sale
                    {
                        id_km = s.id_km,
                        ten_km = s.ten_km,
                        noidung_km = s.ten_km,
                        rate = s.rate,
                        active = s.active
                    };
                    context.khuyenmai.Add(sale);
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

        public JsonResult GetSaleList()
        {
            List<Sale> saleList = new List<Sale>();

            using (var ctx = new AdminDbContext())
            {
                saleList = ctx.khuyenmai.ToList<Sale>();
            }

            if (saleList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = saleList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetSale(string id)
        {
            Sale s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.khuyenmai.Where(t => t.id_km.Equals(id)).FirstOrDefault<Sale>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(Sale t)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.khuyenmai.Where(z => t.id_km.Equals(z.id_km)).FirstOrDefault<Sale>();
                if (current != null)
                {
                    current.ten_km = t.ten_km;
                    current.noidung_km = t.noidung_km;
                    current.rate = t.rate;
                    current.active = t.active;
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
                var current = ctx.khuyenmai.Where(t => t.id_km.Equals(id)).FirstOrDefault<Sale>();
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