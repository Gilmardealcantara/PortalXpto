using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.Models;

namespace XptoPortalApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Get() => Ok(await _userManager.Users.ToAsyncEnumerable().ToList());

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(Auth auth)
        {

            return Ok();
        }
    }
}
