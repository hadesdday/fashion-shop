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
    public class OrderDetailsManagementController : Controller
    {
        public JsonResult Add(OrderDetailsEntity ord)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var orderDetails = new OrderDetailsEntity
                    {
                        id_hoadon = ord.id_hoadon,
                        id_sanpham = ord.id_sanpham,
                        soluong = ord.soluong,
                        ma_mau = ord.ma_mau,
                        ma_size = ord.ma_size
                    };
                    context.chitiethoadon.Add(orderDetails);
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return new JsonHttpStatusResult("Dữ liệu đã tồn tại", HttpStatusCode.Conflict);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new JsonHttpStatusResult("Dữ liệu không hợp lệ", HttpStatusCode.InternalServerError);
                }
            }
            return new JsonHttpStatusResult("success", HttpStatusCode.OK);
        }

        public JsonResult GetOrderDetailsList()
        {
            List<OrderDetailsEntity> detailsList = new List<OrderDetailsEntity>();
            try
            {
                using (var ctx = new AdminDbContext())
                {
                    detailsList = ctx.chitiethoadon.ToList<OrderDetailsEntity>();
                }

                if (detailsList.Count == 0)
                {
                    return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
                }

                var jsonArray = Json(new { data = detailsList }, JsonRequestBehavior.AllowGet);
                return jsonArray;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult GetOrderDetails(int id)
        {
            OrderDetailsEntity s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.chitiethoadon.Where(t => t.id == id).FirstOrDefault<OrderDetailsEntity>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(OrderDetailsEntity s)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.chitiethoadon.Where(z => z.id == s.id).FirstOrDefault<OrderDetailsEntity>();
                int quantity = ctx.sanpham.Where(t => (t.id_sanpham.Equals(s.id_sanpham))).FirstOrDefault<ProductEntity>().soluongton;

                if (current != null && s.soluong <= quantity)
                {
                    ProductEntity p = ctx.sanpham.Where(t => t.id_sanpham.Equals(s.id_sanpham)).FirstOrDefault<ProductEntity>();
                    p.soluongton = quantity - s.soluong;
                    current.id_sanpham = s.id_sanpham;
                    current.soluong = s.soluong;
                    current.ma_mau = s.ma_mau;
                    current.ma_size = s.ma_size;
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
                var current = ctx.chitiethoadon.Where(z => z.id == id).FirstOrDefault<OrderDetailsEntity>();
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