using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.Models;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dbData = await _apps.Select().ToArray();
            var mockData = new List<App> {
                new App { Id = 1, Title = "Checkout", Url = "https://localhost:5056" },
                new App { Id = 2, Title = "Album", Url = "https://localhost:5066" },
                new App { Id = 3, Title = "Pricing", Url = "https://localhost:5076" },
            };
            return Ok(mockData);
        }
    }
}
