using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace App2.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        public IActionResult Index(string token)
        {
            Console.WriteLine("\n\nCHECK_TOKEN");
            Console.WriteLine(token);
            Console.WriteLine("\n\n");
            if (token != null)
                return Ok();
            return Unauthorized();
        }
    }
}