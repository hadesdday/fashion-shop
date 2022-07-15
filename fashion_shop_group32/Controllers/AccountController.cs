using Databases;
using fashion_shop_group32.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                //Session["email"] = email;
                sendMail(email);
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
                if (usr.active == 0)
                {
                    ViewBag.Message = ("Your Account already not active");
                    return View();
                }
                else
                {
                    Session["UserName"] = usr.username.ToString();
                    Session["Role"] = usr.role.ToString();
                    Session["Idkh"] = usr.id_khachhang;
                    Session["pass"] = usr.password.ToString();
                    return RedirectToAction("LoggedIn");
                }
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

            if (checkCus(Int32.Parse(Session["Idkh"].ToString())) != null)
            {
                Session["customer"] = checkCus(Int32.Parse(Session["Idkh"].ToString()));
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
            Object id = Session["Idkh"];
            if (doEditCus(Int32.Parse(id.ToString()), namecus, address, phone, email))
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

        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changePassword(string Oldpassword, string newPassword, string confirmPassword)
        {
            string userId = (string)Session["userId"];
            string dbPassword = (string)Session["pass"];
            if (checkOldPassword(Oldpassword, dbPassword))
            {
                if (doEditPassword(userId, newPassword, confirmPassword))
                {
                    return RedirectToAction("UserInfomation");
                }
                ViewBag.Message = "something is wrong";
                return View();
            }
            else
            {
                ViewBag.Message = "please check your password";
                return View();
            }

        }
        public Boolean doEditCus(int id_kh, string namecus, string address, string phone, string email)
        {
            return Models.Dao.UserDao.updateCustomer(id_kh, namecus, address, phone, email);
        }
        public Boolean checkOldPassword(String password, String dbPassword)
        {
            return Models.Dao.UserDao.verify(dbPassword, password);
        }
        public Boolean doEditPassword(string userId, string password, string confirmPassword)
        {
            if (password.Equals(confirmPassword))
            {
                return Models.Dao.UserDao.updateUserPassword(userId, password);
            }
            return true;
        }
        public ActionResult activeUser(String Token)
        {
            if (active(Token))
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Register");
        }
        public void sendMail(String emailTo)
        {

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new NetworkCredential("chanhhiep2907@gmail.com", "jproyjputuwyutvv");
            //smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.Body = getToken(emailTo);
            mail.From = new MailAddress("chanhhiep2907@gmail.com");
            mail.To.Add(new MailAddress(emailTo));
            mail.Subject = "Active User Account";
            // mail.CC.Add(new MailAddress((string)Session["email"]));

            smtpClient.Send(mail);
        }
        public Boolean active(String token)
        {
            return Models.Dao.UserDao.activeUser(token);
        }
        public string getToken(string email)
        {
            String result = "";
            String rs = Models.Dao.UserDao.getToken(email);
            if (rs == null)
            {
                result = "sorry your email already have in my sytem";
            }
            else
            {
                result = "bạn đã đăng ký thành công vui lòng link vào link sau để active tài khoản :" + " https://localhost:44332/Account/activeUser?token=" + rs;
            }
            return result;
        }
    }
}