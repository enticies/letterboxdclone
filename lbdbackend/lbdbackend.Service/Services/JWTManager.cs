using lbdbackend.Core.Entities;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class JWTManager : IJWTManager {
        private readonly UserManager<AppUser> _userManager;
        private IConfiguration Configuration { get; }

        public JWTManager(UserManager<AppUser> userManager, IConfiguration configuration) {
            _userManager = userManager;
            Configuration = configuration;
        }
        public async Task<string> GenerateToken(AppUser user) {
            List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                };

            IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach (string role in roles) {
                Claim claim = new Claim(ClaimTypes.Role, role);

                claims.Add(claim);
            }

            //SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT: SecurityKey").Value));
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:SecurityKey").Value));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: Configuration.GetSection("JWT:Issuer").Value,
                    audience: Configuration.GetSection("JWT:Audience").Value,
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddHours(8)
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        }

        public string GetUsernameByToken(string token) {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToList().FirstOrDefault(e => e.Type == ClaimTypes.Name).Value;
        }

        public string decodeJWT(string tokenString) {
            var jwtToken = new JwtSecurityToken(tokenString);

            return jwtToken.Subject;
        }
    }
}
