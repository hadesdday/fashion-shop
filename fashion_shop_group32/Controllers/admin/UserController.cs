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
    public class UserController : Controller
    {
        public JsonResult Add(User c)
        {
            using (var context = new AdminDbContext())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var user = new User
                    {
                        username = c.username,
                        password = BCrypt.Net.BCrypt.HashPassword(c.password),
                        role = c.role,
                        email = c.email,
                        id_khachhang = c.id_khachhang,
                        active = c.active
                    };
                    context.User.Add(user);
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

        public JsonResult GetUserList()
        {
            List<User> userList = new List<User>();

            using (var ctx = new AdminDbContext())
            {
                userList = ctx.User.ToList<User>();
            }

            if (userList.Count == 0)
            {
                return new JsonHttpStatusResult("error", HttpStatusCode.InternalServerError);
            }

            var jsonArray = Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }

        public JsonResult GetUser(string id)
        {
            User s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.User.Where(t => t.username.Equals(id)).FirstOrDefault<User>();
            }

            if (s == null)
            {
                return new JsonHttpStatusResult("not found any record", HttpStatusCode.InternalServerError);
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInformation(User t)
        {
            using (var ctx = new AdminDbContext())
            {
                var current = ctx.User.Where(z => t.username.Equals(z.username)).FirstOrDefault<User>();
                if (current != null)
                {
                    current.role = t.role;
                    current.email = t.email;
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
                var current = ctx.User.Where(t => t.username.Equals(id)).FirstOrDefault<User>();
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