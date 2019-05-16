using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Souvenirs.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public decimal OrderItemPrice { get; set; }

        public Souvenir Sourvenir { get; set; }
        public Order Order { get; set; }
    }
}
