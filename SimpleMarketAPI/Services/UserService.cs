using Microsoft.IdentityModel.Tokens;
using SimpleMarketAPI.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace SimpleMarketAPI.Services
{
    public interface IUserService
    {
        public User AuthUser(string username, string password);
        public IEnumerable<User> getAll();
        public User getUser(string username);
    }
    public class UserService : IUserService
    {
        public static string KEY = "skadka ifh sdfgsdkfgsgkfgskfgskaksgksg  khfhksdkf";
        static List<User> LIST_USER = new List<User>();
        public UserService()
        {
            LIST_USER.Add(new User("lenguyenkhoa","12345","Admin"));
            LIST_USER.Add(new User("lenguyenkhiem", "000000", "Employee"));
        }

        public User AuthUser(string username, string password)
        {
            var userTemp = LIST_USER.FirstOrDefault(user=>user.Username==username && user.Password==password); //FIND USER IN LIST USER
            if(userTemp == null) //IF NOT FOUND RETURN NULL
            {
                return null;
            }

            List<Claim> lstClaim = new List<Claim>(); //CLAIM USER INFO
            lstClaim.Add(new Claim(ClaimTypes.Name, userTemp.Username));
            lstClaim.Add(new Claim(ClaimTypes.Role, userTemp.Role));


            //CREATE JWT TOKEN
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var key = Encoding.ASCII.GetBytes(KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(lstClaim.ToArray()),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userTemp.Token = tokenHandler.WriteToken(token);

            return userTemp.withOutPassWord();

        }

        public IEnumerable<User> getAll()
        {
            return LIST_USER;
        }
        public User getUser(string username)
        {
                return LIST_USER.SingleOrDefault(user=>user.Username==username).withOutPassWord();
        }
    }
}
