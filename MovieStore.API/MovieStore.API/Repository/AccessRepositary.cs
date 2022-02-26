using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    public class AccessRepositary : IAccessRepositary
    {
        private readonly UserManager<ApplicationUser> _userManager; // Creating an instance of UserManager using dependency injection to signup a new user
        private readonly SignInManager<ApplicationUser> _signInManager; // Creating an instance of SignInManager using dependency injection to signin a user
        private readonly IConfiguration _configuration; // Creating an instance of IConfiguration using dependency injection to read data from appsettings.json file

        // Constructor to use the dependency injection so to get the instance for UserManager, SignInManager, and IConfiguration
        // Dependency injection allows to get the instance of UserManager, SignInManager, and IConfiguration in the application because we have registered them in the Startup class
        // All the methods in this repository can now use the instances inside the constructor below
        public AccessRepositary(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Creating a new user to the database through user manager and sends the IdentityResult to the AccessController through the interface
        // IdentityResult contains the bool property "succeeded" which is true if user account is created. Otherwise, it is false 
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var newUser = new ApplicationUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
                CreditCard = signUpModel.CreditCard,
                Age = signUpModel.Age
            };
            return await _userManager.CreateAsync(newUser, signUpModel.Password);
        }

        // Signing in the specified username and password using the SignInManager
        // Upon successfull signin, a jwt is generated and send back to AccessController through the interface
        public async Task<string> SignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);

            // Verifying the SignInResult
            if(!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secret"]));

            // generate token using the claims, signinkey, launchSettings file data
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                );

            // returning the generated JWT to the AccessController through the interface
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
