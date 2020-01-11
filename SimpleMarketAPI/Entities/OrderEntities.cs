using SimpleMarketAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMarketAPI.Entities
{
    public class OrderEntities

    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string OrderPaymethod { get; set; }
        public string OrderStatus { set; get; }
        public List<OrderDetailEntities> lstOrderDetail { set; get; }

        public class OrderDetailEntities
        {
            public string ProductId { get; set; }
            public int OrderdetailCount { get; set; }

            public OrderDetailEntities(string productId, int orderdetailCount)
            {
                ProductId = productId;
                OrderdetailCount = orderdetailCount;
            }
        }

    }
}
