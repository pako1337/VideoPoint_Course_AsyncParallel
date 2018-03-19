using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_Async.Models
{
    [Table("ProductCategory", Schema = "Production")]
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }

        public List<ProductSubcategory> ProductSubcategories { get; set; }
    }
}