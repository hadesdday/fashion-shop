using Databases;
using fashion_shop_group32.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                Session["UserName"] = usr.username.ToString();
                Session["Role"] = usr.role.ToString();
                Session["Idkh"] = usr.id_khachhang.ToString();
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
            if (Session["UserName"] != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult UserInfomation()
        {

            if (checkCus(Int32.Parse((string)Session["Idkh"])) != null)
            {
                Session["customer"] = checkCus(Int32.Parse((string)Session["Idkh"]));
            }
            else
            {
                RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult EditInfoCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditInfoCus(string namecus, string address, string phone, string email)
        {
            if (doEditCus(Int32.Parse((string)Session["Idkh"]), namecus, address, phone, email))
            {
                ViewBag.Message = namecus + "sucess register";
                return RedirectToAction("UserInfomation");
            }
            else
            {
                ViewBag.Message = "some thing is wrong";
                return View();
            }
        }
        public Customer checkCus(int idCus)
        {
            return Models.Dao.UserDao.getSelectedCus(idCus);
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
        public Boolean doEditCus(int id_kh, string namecus, string address, string phone, string email)
        {
            return Models.Dao.UserDao.updateCustomer(id_kh, namecus, address, phone, email);
        }

    }
}