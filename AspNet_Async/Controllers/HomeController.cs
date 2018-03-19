using System.Linq;
using System.Web.Mvc;
using AspNet_Async.Models;

namespace AspNet_Async.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BestProducts()
        {
            using (var context = new AdventureContext())
            {
                var products = context.Products.Take(5).ToList();
                var totalProducts = context.Products.Count();

                return View(new BestProductsViewModel
                {
                    Products = products,
                    TotalProductsCount = totalProducts
                });
            }
        }
    }
}