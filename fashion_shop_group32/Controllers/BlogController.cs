using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View("BlogHome");
        }

        public ActionResult SingleBlog()
        {
            return View("SingleBlog");
        }
    }
}