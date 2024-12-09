using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace demoAPI.Model
{
    [Table("Shippers")]
    public class Shipper
    {
        public int ShipperID { get; set; }
        public String CompanyName { get; set; } 
        public String? Phone { get;set; }
    }
}
