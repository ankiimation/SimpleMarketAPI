using System;
using System.Collections.Generic;

namespace SimpleMarketAPI.Models
{
    public partial class OrderDetail
    {
        public int OrderdetailId { get; set; }
        public int? OrderId { get; set; }
        public string ProductId { get; set; }
        public int OrderdetailCount { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
