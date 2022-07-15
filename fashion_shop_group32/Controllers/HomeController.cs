using fashion_shop_group32.Models;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _product;
        public HomeController()
        {
            _product = new MockProduct();
        }

        public ViewResult Index()
        {
            ViewModelIndex viewModel = new ViewModelIndex();
            viewModel.list1 = new MockProduct().GetAllProducts();
            viewModel.list2 = new MockProduct().GetMostSoldProducts();
            viewModel.list3 = new MockProduct().GetRandomProducts();
            viewModel.list4 = new MockProduct().GetLatestProducts();
            return View(viewModel);
        }


    }
}