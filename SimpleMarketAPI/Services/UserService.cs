using Microsoft.IdentityModel.Tokens;
using SimpleMarketAPI.Entities;
using SimpleMarketAPI.Models;
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
        public UserEntities AuthUser(string username, string password);
        public IEnumerable<UserEntities> getAll();
        public UserEntities getUser(string username);
    }
    public class UserService : IUserService
    {
        public static string KEY = "skadka ifh sdfgsdkfgsgkfgskfgskaksgksg  khfhksdkf";
        static SIMPLEMARKETContext context = new SIMPLEMARKETContext();
        static List<UserEntities> LIST_USER = (from user in context.Users select user ).Select(user=>new UserEntities(user.Username,user.Password,user.Roles)).ToList();


        public UserEntities AuthUser(string username, string password)
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

        public IEnumerable<UserEntities> getAll()
        {
            return LIST_USER;
        }
        public UserEntities getUser(string username)
        {
                return LIST_USER.SingleOrDefault(user=>user.Username==username).withOutPassWord();
        }
    }
}
