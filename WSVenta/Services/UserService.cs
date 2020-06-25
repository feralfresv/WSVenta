using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WSVenta.Models;
using WSVenta.Models.Common;
using WSVenta.Models.RequestViewModels;
using WSVenta.Models.Response;
using WSVenta.Tools;

namespace WSVenta.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();
            using (var db = new VentaRealContext())
            {
                string spasssword = Encrypt.GetSHA256(model.Password);
                var usaurio = db.Usuario.Where(o => o.Email == model.Email && 
                                               o.Password == spasssword).FirstOrDefault();

                if (usaurio == null) return null;

                userResponse.Email = usaurio.Email;
                userResponse.Token = GetToken(usaurio);
            }
            return userResponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                    ),
                    Expires = DateTime.UtcNow.AddDays(60),
                    SigningCredentials = 
                    new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
