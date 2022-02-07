using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Models;
using MovieStore.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessRepositary _accessRepositary;

        public AccessController(IAccessRepositary accessRepositary)
        {
            _accessRepositary = accessRepositary;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var newUserResult = await _accessRepositary.SignUpAsync(signUpModel);

            if(newUserResult.Succeeded)
            {
                return Ok(newUserResult.Succeeded);
            }
            return Unauthorized();
        }
    }
}
