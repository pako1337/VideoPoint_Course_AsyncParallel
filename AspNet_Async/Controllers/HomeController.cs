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

        public async Task<ActionResult> BestProductsConcurrentReadersAsync()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var productsCommand = new SqlCommand("SELECT TOP 5 ProductID, Name, ProductNumber FROM Production.Product");
                productsCommand.Connection = connection;
                var productsCountCommand = new SqlCommand("SELECT COUNT(*) FROM Production.Product");
                productsCountCommand.Connection = connection;

                var productsTask = productsCommand.ExecuteReaderAsync()
                    .ContinueWith(t =>
                    {
                        var products = new List<Product>(5);
                        using (var reader = t.Result)
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    ProductID = (int)reader["ProductID"],
                                    Name = (string)reader["Name"],
                                    ProductNumber = (string)reader["ProductNumber"]
                                });
                            }
                        }

                        return products;
                    });

                var productsCountTask = productsCountCommand.ExecuteScalarAsync()
                    .ContinueWith(t => (int)t.Result);

                await Task.WhenAll(productsTask, productsCountTask);

                return View("BestProducts", new BestProductsViewModel
                {
                    Products = productsTask.Result,
                    TotalProductsCount = productsCountTask.Result
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

        public ActionResult BestProductsWithWaitAsync()
        {
            var viewModel = BuildViewModelAsync().Result;
            return View("BestProducts", viewModel);
        }

        public async Task<BestProductsViewModel> BuildViewModelAsync()
        {
            using (var context = new AdventureContext())
            {
                var products = await context.Products.Take(5).ToListAsync().ConfigureAwait(false);
                var totalProducts = await context.Products.CountAsync().ConfigureAwait(false);

                return new BestProductsViewModel
                {
                    Products = products,
                    TotalProductsCount = totalProducts
                };
            }
        }
    }
}