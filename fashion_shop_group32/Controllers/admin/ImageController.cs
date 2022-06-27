using fashion_shop_group32.Context;
using fashion_shop_group32.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers.admin
{
    public class ImageController : Controller
    {
        public JsonResult Upload()
        {
            var image = Request.Files["image"];
            if (image != null && image.ContentLength > 0)
            {
                string filename = image.FileName;
                if (System.IO.File.Exists(Server.MapPath("~/Content/upload/") + image.FileName))
                {
                    Random rd = new Random();
                    int rand = rd.Next(1, 10000);
                    filename = rand + "-" + image.FileName;
                }
                string des = Server.MapPath("~/Content/upload/") + filename;
                image.SaveAs(des);
                string serverPath = "/Content/upload/" + filename;
                return new JsonHttpStatusResult(new { path = serverPath }, HttpStatusCode.OK);
            }
            return new JsonHttpStatusResult("invalid data", HttpStatusCode.InternalServerError);
        }

        public JsonResult Add(Image img)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var image = new Image
                    {
                        link_anh = img.link_anh,
                        id_sanpham = img.id_sanpham
                    };
                    context.hinhanh.Add(image);
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
        public JsonResult GetImageList()
        {
            List<Image> imageList = new List<Image>();

            using (var ctx = new AdminDbContext())
            {
                imageList = ctx.hinhanh.ToList<Image>();
            }

            if (imageList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = imageList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }
        public JsonResult GetImage(int id)
        {
            Image s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.hinhanh.Where(t => t.id_anh == id).FirstOrDefault<Image>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(Image i)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.hinhanh.Where(z => z.id_anh == i.id_anh).FirstOrDefault<Image>();
                if (current != null)
                {
                    current.id_sanpham = i.id_sanpham;
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
                var current = ctx.hinhanh.Where(z => z.id_anh == id).FirstOrDefault<Image>();
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