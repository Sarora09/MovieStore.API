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

            // If Succeeded is true then it will return 200 Ok status code
            if (newUserResult.Succeeded)
            {
                return Ok(newUserResult.Succeeded);
            }

            // If Succeeded is false then it will return 401 Unauthorized status code
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var userToken = await _accessRepositary.SignInAsync(signInModel);

            // if userToken is null or empty then return unuathorized status code 
            if (string.IsNullOrEmpty(userToken))
            {
                return Unauthorized();
            }

            // If userToken contains a value then send the token using 200 Ok status code
            return Ok(userToken);
        }
    }
}
