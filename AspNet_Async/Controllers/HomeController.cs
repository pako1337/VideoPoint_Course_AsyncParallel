using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ActionResult> BestProductsAsync()
        {
            using (var context = new AdventureContext())
            {
                var products = await context.Products.Take(5).ToListAsync();
                var totalProducts = await context.Products.CountAsync();

                return View("BestProducts", new BestProductsViewModel
                {
                    Products = products,
                    TotalProductsCount = totalProducts
                });
            }
        }

        public Task<ActionResult> BestProductsManualAsync()
        {
            using (var context = new AdventureContext())
            {
                return context.Products.Take(5).ToListAsync()
                    .ContinueWith(productListTask =>
                    {
                        return context.Products.CountAsync()
                        .ContinueWith(productsCountTask =>
                        {
                            return (ActionResult)View("BestProducts", new BestProductsViewModel
                            {
                                Products = productListTask.Result,
                                TotalProductsCount = productsCountTask.Result
                            });
                        });
                    }).Result;
            }
        }
    }
}