using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View("CartHome");
        }
    }
}