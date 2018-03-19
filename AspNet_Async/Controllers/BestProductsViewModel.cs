using System.Collections.Generic;
using AspNet_Async.Models;

namespace AspNet_Async.Controllers
{
    public class BestProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int TotalProductsCount { get; set; }
    }
}