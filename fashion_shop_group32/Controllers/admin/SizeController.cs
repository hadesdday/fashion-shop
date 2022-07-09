using fashion_shop_group32.Context;
using fashion_shop_group32.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers.admin.SizeBoard
{
    //[System.Web.Http.Authorize(Roles = "Admin")]
    public class SizeController : Controller
    {
        public JsonResult AddNewSize(Size s)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var size = new Size
                    {
                        ma_sizesp = s.ma_sizesp,
                        ten_sizesp = s.ten_sizesp
                    };
                    context.Size.Add(size);
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
        public JsonResult GetSizeList()
        {
            List<Size> sizeList = new List<Size>();

            using (var ctx = new AdminDbContext())
            {
                sizeList = ctx.Size.ToList<Size>();
            }

            if (sizeList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = sizeList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetSize(string id)
        {
            Size s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.Size.Where(t => t.ma_sizesp.Equals(id)).FirstOrDefault<Size>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(Size s)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.Size.Where(z => z.ma_sizesp.Equals(s.ma_sizesp)).FirstOrDefault<Size>();
                if (current != null)
                {
                    current.ten_sizesp = s.ten_sizesp;
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
                var current = ctx.Size.Where(z => z.ma_sizesp.Equals(id)).FirstOrDefault<Size>();
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