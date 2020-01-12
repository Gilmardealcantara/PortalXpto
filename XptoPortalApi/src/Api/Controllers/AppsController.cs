using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XptoPortalApi.DataAcess.Interfaces;

namespace XptoPortalApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppsController : ControllerBase
    {
        private readonly IAppsRepo _apps;
        public AppsController(IAppsRepo repo)
        {
            _apps = repo;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Get() => Ok(await _apps.Select().ToArray());
    }
}
