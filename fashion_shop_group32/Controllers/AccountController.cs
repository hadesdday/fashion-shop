using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("AccountHome");
        }
    }
}