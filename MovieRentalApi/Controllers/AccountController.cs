using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRental.Contract.Service;
using MovieRental.Contract.Model;

namespace MovieRentalApi.Controllers
{
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;
        readonly ILogger<AccountController> _logger;


        public AccountController(
           IAccountService accountService,
           ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Log in with the user name and password to the API, and get the token.
        /// Take the token returned and use it to authorize to the rest of the API by pressing the Authorize button above.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>Bearer Token</returns>
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var token = await _accountService.CreateToken(loginModel);

                if (String.IsNullOrWhiteSpace(token))
                {
                    return BadRequest();
                }
                else
                    return Ok(token);
            }
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = await _accountService.RefreshToken(User);

            return Ok(token);
        }

        /// <summary>
        /// Creates a new user in the API and logs them in.
        /// </summary>
        /// <param name="registerModel">Registration information for user.  Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character.</param>
        /// <returns>Bearer Token</returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var (identityResult, token) = await _accountService.Register(registerModel);
                if (identityResult.Succeeded)
                {
                    return Ok(token);
                }
                else
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
