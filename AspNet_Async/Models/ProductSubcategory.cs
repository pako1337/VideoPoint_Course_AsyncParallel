using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_Async.Models
{
    [Table("ProductSubcategory", Schema = "Production")]
    public class ProductSubcategory
    {
        [Key]
        public int ProductSubcategoryID { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}