using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMarketAPI.Services;

namespace SimpleMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IUserService userService;

        public TestController(IUserService userService)
        {
            this.userService = userService;
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

        [Authorize(Roles ="Admin")]
        public IActionResult getAll()
        {
            return  Ok(userService.getAll());
        }
    }
}