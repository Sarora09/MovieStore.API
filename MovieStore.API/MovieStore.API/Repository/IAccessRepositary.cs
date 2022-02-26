using Microsoft.AspNetCore.Identity;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    // Interface for AccessRepository
    public interface IAccessRepositary
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);

        Task<string> SignInAsync(SignInModel signInModel);
    }
}
