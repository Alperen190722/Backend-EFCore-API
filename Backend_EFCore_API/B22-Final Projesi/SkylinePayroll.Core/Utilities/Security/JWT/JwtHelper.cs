using Microsoft.Extensions.Configuration;
using SkylinePayroll.Core.Entities;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Utilities.Security.Encryption;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateToken(IUserDetailed user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtToken);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }
        private IEnumerable<Claim> SetClaims(IUserDetailed user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            if (user == null)
            {
                claims.Add(new Claim(ClaimTypes.Name, "Bilinmeyen Kullanıcı"));
                return claims;
            }
            claims.Add(new Claim("EmployeeId", user.EmployeeId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim("Department", user.DepartmentName ?? "Genel"));
            string roleName = user.RoleName ?? "Rol Yok";
            int hierarchy = user.HierarchyLevel; 

            claims.Add(new Claim(ClaimTypes.Role, roleName));
            claims.Add(new Claim("HierarchyLevel", hierarchy.ToString()));
            claims.Add(new Claim("Status", "1"));

            if (operationClaims != null)
            {
                foreach (var oc in operationClaims)
                {
                    if (!string.IsNullOrEmpty(oc.Name))
                        claims.Add(new Claim(ClaimTypes.Role, oc.Name));
                }
            }

            return claims;
        }
    }
}
