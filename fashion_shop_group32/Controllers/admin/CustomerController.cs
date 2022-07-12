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
    public class CustomerController : Controller
    {
        public JsonResult Add(CustomerEntity c)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var customer = new CustomerEntity
                    {
                        ten_kh = c.ten_kh,
                        diachi = c.diachi,
                        sodt = c.sodt,
                        email = c.email
                    };
                    context.khachhang.Add(customer);
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

        public JsonResult GetCustomerList()
        {
            List<CustomerEntity> customerList = new List<CustomerEntity>();

            using (var ctx = new AdminDbContext())
            {
                customerList = ctx.khachhang.ToList<CustomerEntity>();
            }

            if (customerList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = customerList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetCustomer(int id)
        {
            CustomerEntity s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.khachhang.Where(t => t.id_khachhang == id).FirstOrDefault<CustomerEntity>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(CustomerEntity t)
        {
            if (!ModelState.IsValid) return new JsonHttpStatusResult("invalid data", HttpStatusCode.BadRequest);

            using (var ctx = new AdminDbContext())
            {
                var current = ctx.khachhang.Where(z => t.id_khachhang == z.id_khachhang).FirstOrDefault<CustomerEntity>();
                if (current != null)
                {
                    current.ten_kh = t.ten_kh;
                    current.diachi = t.diachi;
                    current.sodt = t.sodt;
                    current.email = t.email;
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
                var current = ctx.khachhang.Where(t => t.id_khachhang == id).FirstOrDefault<CustomerEntity>();
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