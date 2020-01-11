using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SimpleMarketAPI.Models;
using SimpleMarketAPI.Services;

namespace SimpleMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private static SIMPLEMARKETContext context = new SIMPLEMARKETContext();

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult getAll()
        {
            return Ok(userService.getAll());
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult login([FromForm] string username, [FromForm]string password)
        {

            var userTemp = userService.AuthUser(username, password);

            if (userTemp == null)
                return BadRequest(new { message = "Username password fail" });
            return Ok(userTemp);

        }
        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult signUp([FromForm] string username, [FromForm]string password)
        {

          
                Users userTemp = new Users();
                userTemp.Username = username;
                userTemp.Password = password;

            if ((from user in context.Users select user).ToList().FirstOrDefault(user => user.Username.Equals(username)) == null)
            {
                context.Users.Add(userTemp);
                context.SaveChanges();
                return Ok("Đăng kí thành công");
            }
            return Forbid("Đăng kí thất bại");

        }


     
    }
}