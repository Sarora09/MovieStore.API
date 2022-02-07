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
    }
}
