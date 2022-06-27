using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View("CheckoutHome");
        }
    }
}