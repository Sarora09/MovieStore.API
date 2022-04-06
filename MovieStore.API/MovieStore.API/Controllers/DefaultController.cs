using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// This class is developed for the Azure deployment
namespace MovieStore.API.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi =true)]
        [HttpGet("/")]
        public IActionResult Index()
        {
            return new RedirectResult("/swagger");
        }
    }
}
