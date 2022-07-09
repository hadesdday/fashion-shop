using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult Index()
        {
            return View("WishlistHome");
        }
    }
}