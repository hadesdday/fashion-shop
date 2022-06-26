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
    public class OrderManagementController : Controller
    {
        public JsonResult Add(Order o)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var order = new Order
                    {
                        id_khachHang = o.id_khachHang,
                        id_magg = o.id_magg,
                        mapttt = o.mapttt,
                        trigia = o.trigia,
                        trangthai = o.trangthai
                    };
                    context.hoadon.Add(order);
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return new JsonHttpStatusResult("existed data\n" + ex, HttpStatusCode.Conflict);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new JsonHttpStatusResult("invalid data", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("success", HttpStatusCode.OK);
        }

        public JsonResult GetOrderList()
        {
            List<Order> orderList = new List<Order>();

            using (var ctx = new AdminDbContext())
            {
                orderList = ctx.hoadon.ToList<Order>();
            }

            if (orderList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = orderList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetOrder(int id)
        {
            Order s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.hoadon.Where(t => t.id_hoadon == id).FirstOrDefault<Order>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(Order t)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.hoadon.Where(z => t.id_hoadon == z.id_hoadon).FirstOrDefault<Order>();
                if (current != null)
                {
                    current.id_magg = t.id_magg;
                    current.mapttt = t.mapttt;
                    current.trigia = t.trigia;
                    current.trangthai = t.trangthai;
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
                var current = ctx.hoadon.Where(t => t.id_hoadon == id).FirstOrDefault<Order>();
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