using System;
using System.Collections.Generic;

namespace SimpleMarketAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            CartDetail = new HashSet<CartDetail>();
            Orders = new HashSet<Orders>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }

        public virtual ICollection<CartDetail> CartDetail { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
