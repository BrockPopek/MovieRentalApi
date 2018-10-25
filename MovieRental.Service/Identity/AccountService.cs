using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Omu.ValueInjecter;
using MovieRental.Contract.DataAccess;
using MovieRental.Contract.Service;
using MovieRental.DataAccess.Entities;
using MovieRental.Contract.Model;


namespace MovieRental.Service.Identity
{
    public class AccountService : IAccountService
    {
        private readonly IApplicationUserAccessor _applicationUserAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
           IApplicationUserAccessor applicationUserAccessor,
           IConfiguration configuration,
           ILogger<AccountService> logger)
        {
            _applicationUserAccessor = applicationUserAccessor;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CreateToken(LoginModel loginModel)
        {
            var token = string.Empty;

            var loginResult = await _applicationUserAccessor.PasswordSignInAsync(loginModel);

            if (loginResult.Succeeded)
            {
                var user = await _applicationUserAccessor.FindByNameAsync(loginModel.UserName);
                token = await GetToken(user);
            }

            return token;
        }


        public async Task<string> RefreshToken(ClaimsPrincipal userClaim)
        {
            var user = await _applicationUserAccessor.FindByNameAsync(
                userClaim.Identity.Name ??
                userClaim.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );
            return await GetToken(user);
        }

        public async Task<(IdentityResult identityResult, string token)> Register(RegisterModel registerModel)
        {
            var token = string.Empty;

            var user = new ApplicationUser();
            user.InjectFrom(registerModel);

            var identityResult = await _applicationUserAccessor.CreateAsync(user, registerModel);

            if (identityResult.Succeeded)
            {
                identityResult = await _applicationUserAccessor.AddRoleToUser(user, registerModel);

                if (identityResult.Succeeded)
                {
                    await _applicationUserAccessor.SignInAsync(user);
                    token = await GetToken(user);
                }
            }

            return (identityResult: identityResult, token: token);
        }

        private async Task<string> GetToken(IdentityUser user)
        {
            var utcNow = DateTime.UtcNow;


            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
                        new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
            };

            var userRoles = await _applicationUserAccessor.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("Tokens:Lifetime")),
                audience: _configuration.GetValue<String>("Tokens:Audience"),
                issuer: _configuration.GetValue<String>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}
