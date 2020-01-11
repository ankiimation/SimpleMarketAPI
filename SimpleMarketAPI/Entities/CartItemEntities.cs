using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMarketAPI.Entities
{
    public class CartItemEntities
    {
        public string UserName { set; get; }
        public string ProductID { set; get; }
        public int Count { set; get; }

        public CartItemEntities(string userName, string productID, int count)
        {
            UserName = userName;
            ProductID = productID;
            Count = count;
        }
    }
}
