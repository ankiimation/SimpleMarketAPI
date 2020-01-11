using System;
using System.Collections.Generic;

namespace SimpleMarketAPI.Models
{
    public partial class Products
    {
        public Products()
        {
            CartDetail = new HashSet<CartDetail>();
            OrderDetail = new HashSet<OrderDetail>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProdcutPrice { get; set; }

        public virtual ICollection<CartDetail> CartDetail { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
