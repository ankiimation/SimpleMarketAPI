using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMarketAPI.Entities
{
    public class Product
    {
        public string productId { set; get; }
        public string productName { set; get; }
        public string productDescription { set; get; }
        public int prodcutPrice { set; get; }

        public Product(string productId, string productName, string productDescription, int prodcutPrice)
        {
            this.productId = productId;
            this.productName = productName;
            this.productDescription = productDescription;
            this.prodcutPrice = prodcutPrice;
        }
    }
}
