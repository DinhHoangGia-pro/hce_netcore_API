using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace demoAPI.Model
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public String ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public String? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set;}
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get;set; }
        public bool Discontinued { get; set; }
    }
}
