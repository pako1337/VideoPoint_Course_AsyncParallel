using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_Async.Models
{
    [Table("Product", Schema = "Production")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public short SafetyStockLevel { get; set; }
        public int? ProductSubcategoryID { get; set; }

        [ForeignKey(nameof(ProductSubcategoryID))]
        public ProductSubcategory ProductSubcategory { get; set; }
    }
}