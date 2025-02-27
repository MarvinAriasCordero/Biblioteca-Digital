using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Biblioteca_digital.Utilitarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca_digital.Servicios
{
    public class ServicioToken : IServicioToken
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly JWTOptions _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ServicioToken(UserManager<Usuario> userManager, IOptions<JWTOptions> jWTOptions,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            this._jwtOptions = jWTOptions.Value;
            _roleManager = roleManager;
        }

        public async Task<string> WriteToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name,user.UserName)
        };

            var userRoles = await _userManager.GetRolesAsync(user);
            claimList.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
