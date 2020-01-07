using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMarketAPI.Entities
{
    public class User
    { 
        public string Username { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        public string Token { set; get; }

        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public User withOutPassWord()
        {
            User userTemp = this;
            userTemp.Password = null;
            return userTemp;
        }
    }
}
