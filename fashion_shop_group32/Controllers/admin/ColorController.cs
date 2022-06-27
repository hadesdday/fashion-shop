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
    public class ColorController : Controller
    {
        public JsonResult Add(Color cl)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var color = new Color
                    {
                        ma_mausp = cl.ma_mausp,
                        mausp = cl.mausp
                    };
                    context.mausanpham.Add(color);
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

        public JsonResult GetColorList()
        {
            List<Color> colorList = new List<Color>();

            using (var ctx = new AdminDbContext())
            {
                colorList = ctx.mausanpham.ToList<Color>();
            }

            if (colorList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = colorList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetColor(string id)
        {
            Color s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.mausanpham.Where(t => t.ma_mausp.Equals(id)).FirstOrDefault<Color>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(Color s)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.mausanpham.Where(z => z.ma_mausp.Equals(s.ma_mausp)).FirstOrDefault<Color>();
                if (current != null)
                {
                    current.mausp = s.mausp;
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
                var current = ctx.mausanpham.Where(z => z.ma_mausp.Equals(id)).FirstOrDefault<Color>();
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