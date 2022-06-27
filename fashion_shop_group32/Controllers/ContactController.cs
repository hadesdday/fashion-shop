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
    }
}