using System.Linq;
using Auth.API.Models;
using Auth.API.Helpers;
using Auth.API.Presistence;
using Auth.API.Security;
using Microsoft.Extensions.Options;

namespace Auth.API.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        string CreateToken(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;
        private readonly AppSettings _appSettings;
        public AuthService(
            DataContext context,
            IPasswordHasher passwordHasher,
            ITokenHandler tokenHandler,
            IOptions<AppSettings> appSettings
        )
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!_passwordHasher.VerifyPasswordHash(password, user.pwdHash))
                return null;

            // authentication successful
            return user;
        }

        public string CreateToken(User user)
        {
            return _tokenHandler.Generate(_appSettings.Secret, user);
        }
    }
}