using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieStore.API.Data;
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
        private readonly IMapper _mapper; // Creating an instance of IMapper using dependency injection

        // Constructor to use the dependency injection so to get the instance for UserManager, SignInManager, and IConfiguration
        // Dependency injection allows to get the instance of UserManager, SignInManager, and IConfiguration in the application because we have registered them in the Startup class
        // All the methods in this repository can now use the instances inside the constructor below
        public AccessRepositary(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        // Creating a new user to the database through user manager and sends the IdentityResult to the AccessController through the interface
        // IdentityResult contains the bool property "succeeded" which is true if user account is created. Otherwise, it is false 
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var newUser = new ApplicationUser()
            {
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
            };
            // returns the IdentityResult
            var result = await _userManager.CreateAsync(newUser, signUpModel.Password);
            return result;

        }

        // Updates a user record in the database if the id exists. Otherwise, it throws an exception
        public async Task<IdentityResult> UpdateUserAsync(UserModel userModel, string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            existingUser.FirstName = userModel.FirstName;
            existingUser.LastName = userModel.LastName;
            existingUser.Email = userModel.Email;
            existingUser.UserName = userModel.Email;
            existingUser.CreditCard = userModel.CreditCard;
            existingUser.Age = userModel.Age;
            var result = await _userManager.UpdateAsync(existingUser);

            // For password update, we have to to specially use the ResetPassword method which will give errors only related to password update
            // Therefore, first check if the user details are all updated successfully and only then update the password
            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var passwordStatus = await _userManager.ResetPasswordAsync(existingUser, token, userModel.Password);
                return passwordStatus;
            }
            else
            {
                // If there is a problem in updating user details (except the password) then send the errors for it
                // result is an IdentityResult type that will contain errors array
                return result;
            }
        }

        // Signing in the specified username and password using the SignInManager
        // Upon successfull signin, a jwt is generated and send back to AccessController through the interface
        public async Task<string> SignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);

            // Verifying the SignInResult
            if (!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secret"]));

            // Generate token using the claims, signinkey, launchSettings file data
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

        // Get the user whose email matches with the email in the request
        public async Task<ApplicationUser> FindUserAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        // Get the user whose id matches with the id in the request
        public async Task<UserModel> FindUserWithIDAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserModel>(result);
        }

        // Get the list of all the users except the admin record
        public async Task<List<AllUsersModel>> GetAllUsersAsync()
        {
            var retrievedRecords = await _userManager.Users.Where(x=>(x.Email!="admin_access@test.com")).ToListAsync();
            return _mapper.Map<List<AllUsersModel>>(retrievedRecords);
        }

        // Deletes a user record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<string> DeleteUserAsync(string id)
        {
            try
            {
                var movieRecord = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(movieRecord);
                return id;
            }
            catch
            {
                return "0";
            }

        }

        // Creating a new user to the database through user manager and sends the IdentityResult to the AccessController through the interface
        // IdentityResult contains the bool property "succeeded" which is true if user account is created. Otherwise, it is false 
        public async Task<IdentityResult> AdminDashboardSignUpAsync(AdminDashboardSignUpModel adminDashboardSignUpModel)
        {
            var newUser = new ApplicationUser()
            {
                FirstName = adminDashboardSignUpModel.FirstName,
                LastName = adminDashboardSignUpModel.LastName,
                Email = adminDashboardSignUpModel.Email,
                UserName = adminDashboardSignUpModel.Email,
                CreditCard = adminDashboardSignUpModel.CreditCard,
                Age = adminDashboardSignUpModel.Age
            };
            // returns the IdentityResult
            var result = await _userManager.CreateAsync(newUser, adminDashboardSignUpModel.Password);
            return result;
        }
    }
}
