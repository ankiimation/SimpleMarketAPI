using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMarketAPI.Entities
{
    public class UserEntities
    { 
        public string Username { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        public string Token { set; get; }

        public UserEntities(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public UserEntities withOutPassWord()
        {
            UserEntities userTemp = this;
            userTemp.Password = null;
            return userTemp;
        }
    }
}
