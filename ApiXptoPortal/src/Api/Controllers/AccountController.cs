using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using XptoPortalApi.Models;
using XptoPortalApi.Services.Utils;

namespace XptoPortalApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _userManager.Users.ToAsyncEnumerable().ToList());

        [HttpPost("Login"), AllowAnonymous]
        public IActionResult Login(Auth auth)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == auth.Email);
            if (user == null)
                return Unauthorized("User Not Found!");

            string secret = _config["Application:SECRET_KEY"];
            string issuer = _config["Application:JWT_ISSUE"];
            string audience = _config["Application:JWT_AUDIENCE"];

            user.GenerateToken(secret, issuer, audience);
            Console.WriteLine(JsonService.Serialize(user));

            return Ok(user);
        }
    }
}
