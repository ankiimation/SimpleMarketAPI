using System;
using System.Collections.Generic;

namespace SimpleMarketAPI.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string Username { get; set; }
        public string OrderPaymethod { get; set; }
        public DateTime OrderCreatedtime { get; set; }
        public string OrderStatus { get; set; }

        public virtual Users UsernameNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
