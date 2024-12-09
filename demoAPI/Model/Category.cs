using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace demoAPI.Model
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }
        public String? Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
