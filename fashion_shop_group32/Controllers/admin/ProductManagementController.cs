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
    public class ProductManagementController : Controller
    {
        public JsonResult Add(ProductEntity p)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var product = new ProductEntity
                    {
                        id_sanpham = p.id_sanpham,
                        ten_sp = p.ten_sp,
                        ma_loaisp = p.ma_loaisp,
                        ma_mau = p.ma_mau,
                        ma_size = p.ma_size,
                        gia = p.gia,
                        loai = p.loai,
                        id_km = p.id_km,
                        thuonghieu = p.thuonghieu,
                        soluongton = p.soluongton,
                        mota = p.mota,
                        active = p.active,

                    };
                    context.sanpham.Add(product);
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

        public JsonResult GetProductList()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            using (var ctx = new AdminDbContext())
            {
                productList = ctx.sanpham.ToList<ProductEntity>();
            }

            if (productList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = productList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }
        public JsonResult GetProduct(string id)
        {
            ProductEntity s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.sanpham.Where(t => t.id_sanpham.Equals(id)).FirstOrDefault<ProductEntity>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(ProductEntity t)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.sanpham.Where(z => t.id_sanpham.Equals(z.id_sanpham)).FirstOrDefault<ProductEntity>();
                if (current != null)
                {
                    current.ten_sp = t.ten_sp;
                    current.ma_loaisp = t.ma_loaisp;
                    current.ma_mau = t.ma_mau;
                    current.ma_size = t.ma_size;
                    current.gia = t.gia;
                    current.loai = t.loai;
                    current.id_km = t.id_km;
                    current.thuonghieu = t.thuonghieu;
                    current.soluongton = t.soluongton;
                    current.mota = t.mota;
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
                var current = ctx.sanpham.Where(t => t.id_sanpham.Equals(id)).FirstOrDefault<ProductEntity>();
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