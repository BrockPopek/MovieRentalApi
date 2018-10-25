using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using MovieRental.Contract.Model;
using MovieRental.DataAccess.Entities;
using MovieRental.Contract.DataAccess;

namespace MovieRental.DataAccess.Accessor
{
    public class ApplicationUserAccessor : IApplicationUserAccessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserAccessor(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(LoginModel loginModel)
        {
            return await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(IdentityUser user)
        {
            return await _userManager.GetRolesAsync((ApplicationUser)user);
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, RegisterModel registerModel)
        {
            return await _userManager.CreateAsync((ApplicationUser)user, registerModel.Password);
        }

        public async Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return await _userManager.UpdateAsync((ApplicationUser)user);
        }

        public async Task SignInAsync(IdentityUser user)
        {
            await _signInManager.SignInAsync((ApplicationUser)user, isPersistent: false);
        }

        public async Task<IdentityResult> AddRoleToUser(IdentityUser user, RegisterModel registerModel)
        {
            return await _userManager.AddToRoleAsync((ApplicationUser)user, registerModel.UserRole.ToString());
        }
    }
}
