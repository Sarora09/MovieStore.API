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

        Task<IdentityResult> UpdateUserAsync(UserModel userModel, string id);

        Task<ApplicationUser> FindUserAsync(string email);

        Task<UserModel> FindUserWithIDAsync(string id);

        Task<List<AllUsersModel>> GetAllUsersAsync();

        Task<string> DeleteUserAsync(string id);
    }
}
