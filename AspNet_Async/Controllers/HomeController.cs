using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public async Task<ActionResult> BestProductsConcurrentQueriesAsync()
        {
            using (var context = new AdventureContext())
            {
                var productsTask = context.Products.Take(5).ToListAsync();
                var totalProductsTask = context.Products.CountAsync();

                await Task.WhenAll(productsTask, totalProductsTask);

                return View("BestProducts", new BestProductsViewModel
                {
                    Products = productsTask.Result,
                    TotalProductsCount = totalProductsTask.Result
                });
            }
        }

        public Task<ActionResult> BestProductsManualAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<ActionResult>();

            var context = new AdventureContext();
            context.Products.Take(5).ToListAsync()
                .ContinueWith(productListTask =>
                {
                    context.Products.CountAsync()
                    .ContinueWith(productsCountTask =>
                    {
                        context.Dispose();
                        taskCompletionSource.SetResult(
                            View("BestProducts", new BestProductsViewModel
                            {
                                Products = productListTask.Result,
                                TotalProductsCount = productsCountTask.Result
                            }));
                    });
                });

            return taskCompletionSource.Task;
        }
    }
}