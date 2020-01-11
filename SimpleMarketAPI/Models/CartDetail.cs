using System;
using System.Collections.Generic;

namespace SimpleMarketAPI.Models
{
    public partial class CartDetail
    {
        public int CartdetailId { get; set; }
        public string Username { get; set; }
        public string ProductId { get; set; }
        public int CartdetailCount { get; set; }

        public virtual Products Product { get; set; }
        public virtual Users UsernameNavigation { get; set; }
    }
}
