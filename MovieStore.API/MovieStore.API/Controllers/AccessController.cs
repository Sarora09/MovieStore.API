using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class AccessController : ControllerBase
    {
        private readonly IAccessRepositary _accessRepositary;

        public AccessController(IAccessRepositary accessRepositary)
        {
            _accessRepositary = accessRepositary;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        
        //[Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _accessRepositary.GetAllUsersAsync();
            return Ok(userList);
        }

        /// <summary>
        /// Create a user
        /// </summary>
         
        [HttpPost("")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var newUserResult = await _accessRepositary.SignUpAsync(signUpModel);

            // If the client account is created, send 
            if(newUserResult.Succeeded)
            {
                //return Ok(newUserResult.Succeeded);
                var userCreated = await _accessRepositary.FindUserAsync(signUpModel.Email);
                return Ok(userCreated.Id);
            }
            else
            {
                return Unauthorized(newUserResult.Errors);
            }
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
         
        // Used when login on front end
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var result = await _accessRepositary.FindUserWithIDAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Update a user by id
        /// </summary>
         
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserModel userModel)
        {
            var result = await _accessRepositary.UpdateUserAsync(userModel, id);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            else
            {
                return StatusCode(500, result.Errors);
            }
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
         
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var deletedId = await _accessRepositary.DeleteUserAsync(id);
            if (deletedId == "0")
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Authenticate a user by email and password
        /// </summary>
         
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var userToken = await _accessRepositary.SignInAsync(signInModel);

            // If userToken is null or empty then return unuathorized status code 
            if (string.IsNullOrEmpty(userToken))
            {
                return Unauthorized();
            }

            // If userToken contains a value then send the token using 200 Ok status code
            return Ok(userToken);
        }

        /// <summary>
        /// Get user id by user email
        /// </summary>

        [ApiExplorerSettings(IgnoreApi = true)]
        // Used when register on front end
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            var result = await _accessRepositary.FindUserAsync(email);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Id);
        }  
    }
}
