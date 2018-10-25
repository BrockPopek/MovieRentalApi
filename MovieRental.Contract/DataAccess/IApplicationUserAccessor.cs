using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.DataAccess
{
    public interface IApplicationUserAccessor
    {
        /// <summary>
        /// Sign in with password.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<SignInResult> PasswordSignInAsync(LoginModel loginModel);

        /// <summary>
        /// Find the application user by UserName.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IdentityUser> FindByNameAsync(string userName);

        /// <summary>
        /// Create the user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(IdentityUser user, RegisterModel registerModel);

        /// <summary>
        /// Update the User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityResult> UpdateAsync(IdentityUser user);

        /// <summary>
        /// Sign in the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task SignInAsync(IdentityUser user);

        /// <summary>
        ///  Add a role for the user to be used for authorization.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        Task<IdentityResult> AddRoleToUser(IdentityUser user, RegisterModel registerModel);

        /// <summary>
        /// Get roles for the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRolesAsync(IdentityUser user);
    }
}
