using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.Service
{
    public interface IAccountService
    {
        /// <summary>
        /// Create a JWT token to login in the API with.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<string> CreateToken(LoginModel loginModel);

        /// <summary>
        /// Refresh the JWT token when needed.
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        Task<string> RefreshToken(ClaimsPrincipal userClaim);

        /// <summary>
        /// Registers a new user by creating one in the system and logging them in at the same time.
        /// It will return an error in the IdentityResult if one occurs.
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        Task<(IdentityResult identityResult, string token)> Register(RegisterModel registerModel);
    }
}
