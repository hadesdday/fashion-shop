using fashion_shop_group32.Context;
using fashion_shop_group32.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace fashion_shop_group32.Controllers.admin
{
    public class AdminController : Controller
    {
        public Boolean IsAdmin()
        {
            try
            {
                User u = (User)Session["superadmin"];
                if (u != null && u.role.Equals("admin"))
                {
                    return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                return false;
            }
        }

        public ActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View();
        }
        public ActionResult Login()
        {
            if (IsAdmin()) return RedirectToAction("Index");
            else if (!IsAdmin()) return View("Login");
            return View("Login");
        }
        public ActionResult Order()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("OrderManagement");
        }
        public ActionResult Product()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("ProductManagement");
        }
        public ActionResult UserManagement()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("UserManagement");
        }
        public ActionResult Customer()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("CustomerManagement");
        }
        public ActionResult Sales()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("SalesManagement");
        }
        public ActionResult Review()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("ReviewManagement");
        }
        public ActionResult PaymentMethod()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("PaymentMethodManagement");
        }
        public ActionResult SizeBoard()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("SizeBoardManagement");
        }
        public ActionResult ColorBoard()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("ColorBoardManagement");
        }
        public ActionResult ProductType()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("ProductTypeManagement");
        }
        public ActionResult Image()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View("Image");
        }
        static bool Verify(string username, string password)
        {
            try
            {
                using (var ctx = new AdminDbContext())
                {
                    User u = ctx.User.Where(t => t.username.Equals(username)).FirstOrDefault<User>();
                    var hashedPassword = u.password;
                    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public JsonResult LoginAdmin()
        {
            string username = Request["username"];
            string password = Request["password"];
            User s = null;
            using (var ctx = new AdminDbContext())
            {
                s = ctx.User.Where(t => (t.username.Equals(username))).FirstOrDefault<User>();
            }

            if (!Verify(username, password))
            {
                return new JsonHttpStatusResult("Error occurred", HttpStatusCode.InternalServerError);
            }
            else
            {
                string role = s.role;
                if (!s.role.Equals("admin"))
                {
                    ViewBag.error = "Access Denied";
                    return new JsonHttpStatusResult("Access Denied", HttpStatusCode.Forbidden);
                }
                else
                {
                    s.password = null;
                    Session["superadmin"] = s;
                    return new JsonHttpStatusResult("OK", HttpStatusCode.OK);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}