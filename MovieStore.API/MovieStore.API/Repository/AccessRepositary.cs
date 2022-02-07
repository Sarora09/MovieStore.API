using Microsoft.AspNetCore.Identity;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    public class AccessRepositary : IAccessRepositary
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccessRepositary(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

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
    }
}
