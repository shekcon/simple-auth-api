using System.Linq;
using Auth.API.Models;
using Auth.API.Helpers;
using Auth.API.Presistence;
using Auth.API.Security;
using WebApi.Helpers;
using System.Collections.Generic;
using Auth.API.Resources;
using System.Text.RegularExpressions;

namespace Auth.API.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(int id, UserResource user);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IPasswordHasher _passwordHasher;

        public UserService(DataContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.username == user.username))
                throw new AppException("Username \"" + user.username + "\" is already taken");

            user.pwdHash = _passwordHasher.HashPassword(password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(int id, UserResource user)
        {
            var currentUser = _context.Users.Find(id);

            if (currentUser == null)
                throw new AppException("User not found");

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(user.password))
            {
                currentUser.pwdHash = _passwordHasher.HashPassword(user.password);
            }
            if(!string.IsNullOrWhiteSpace(user.email) && Regex.IsMatch(user.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)){
                currentUser.email = user.email;
            }
            _context.Users.Update(currentUser);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}