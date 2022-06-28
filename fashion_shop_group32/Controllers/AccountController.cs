using fashion_shop_group32.Models;
using System;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(String username, String password, String confirmPassword, String email, String ten_kh, String diachi, String sodt)
        {
            if (doRegister(username, password, confirmPassword, email, ten_kh, diachi, sodt))
            {
                ViewBag.Message = username + "sucess register";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "username is exist in system";
                return View();
            }

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            Users usr = checkLogin(username, password);
            if (usr != null)
            {
                //set session
                Session["UserId"] = usr.idUser.ToString();
                Session["UserName"] = usr.userName.ToString();
                Session["Role"] = usr.role.ToString();
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ViewBag.Message = ("User Name or Passsword Is Wrong");
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public Users checkLogin(String username, String password)
        {
            return Models.Dao.UserDao.checkLogin(username, password);
        }
        public Boolean doRegister(String username, String password, String confirmPassword, String email, String ten_kh, String diachi, String sodt)
        {
            if (password.Equals(confirmPassword))
            {
                return Models.Dao.UserDao.register(username, password, email, ten_kh, diachi, sodt);
            }
            return true;
        }


    }
}