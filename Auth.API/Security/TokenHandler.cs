using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Auth.API.Models;

namespace Auth.API.Security
{
    /// <sumary> 
    /// Generate jsonwebtoken base on role of user
    /// </sumary>

    public interface ITokenHandler
    {
        string Generate(string sercet, User user);
    }
    
    public class TokenHandler : ITokenHandler
    {
        public string Generate(string sercet, User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(sercet);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString()),
                    new Claim(ClaimTypes.Role, ((Role)user.role).ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}