using fashion_shop_group32.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View("ContactPage");
        }
        public void sendMail(String from, String subject, String content, String name, String phone)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new NetworkCredential("chanhhiep2907@gmail.com", "jproyjputuwyutvv");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.Body = "-From : " + from + "\n -Subject: " + subject + "\n - Tên : " + name + "\n - Số điện thoại " + phone + "\n - Nội dung : " + content;
            mail.From = new MailAddress("chanhhiep2907@gmail.com");
            mail.To.Add(new MailAddress("chanhhiep2907@gmail.com"));
            mail.Subject = "Bạn có tin nhắn mới từ khách hàng";

            smtpClient.Send(mail);
        }

        public JsonResult SubmitProblem(String from, String subject, String content, String name,string phone)
        {
            try
            {
                sendMail(from, subject, content, name,phone);
                return new JsonHttpStatusResult("Success", HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult("Send error", HttpStatusCode.InternalServerError);
            }
        }
    }
}