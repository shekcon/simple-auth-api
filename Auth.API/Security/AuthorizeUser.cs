using System;
using System.Linq;
using System.Security.Claims;

namespace Auth.API.Security
{
    public class AuthorizeUser
    {
        public static bool isMatchRole(ClaimsPrincipal User, string Role)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            return roles[0].Value == Role;
        }

        public static bool isMatchID(ClaimsPrincipal User, int id)
        {
            return id == Int32.Parse(User.Identity.Name);
        }
    }
}