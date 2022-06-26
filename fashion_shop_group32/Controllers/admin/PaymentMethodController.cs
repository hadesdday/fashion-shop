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
    public class PaymentMethodController : Controller
    {
        public JsonResult Add(PaymentMethod p)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var method = new PaymentMethod
                    {
                        mapttt = p.mapttt,
                        tenpttt = p.tenpttt
                    };
                    context.thanhtoan.Add(method);
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

        public JsonResult GetPaymentMethodList()
        {
            List<PaymentMethod> methodList = new List<PaymentMethod>();

            using (var ctx = new AdminDbContext())
            {
                methodList = ctx.thanhtoan.ToList<PaymentMethod>();
            }

            if (methodList.Count == 0)
            {
                return new JsonHttpStatusResult("not found", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = methodList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetPaymentMethod(string id)
        {
            PaymentMethod s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.thanhtoan.Where(t => t.mapttt.Equals(id)).FirstOrDefault<PaymentMethod>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(PaymentMethod t)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.thanhtoan.Where(z => z.mapttt.Equals(t.mapttt)).FirstOrDefault<PaymentMethod>();
                if (current != null)
                {
                    current.tenpttt = t.tenpttt;
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
                var current = ctx.thanhtoan.Where(z => z.mapttt.Equals(id)).FirstOrDefault<PaymentMethod>();
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