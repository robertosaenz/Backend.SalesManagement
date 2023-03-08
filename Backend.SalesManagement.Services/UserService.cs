using Backend.SalesManagement.Models;
using Backend.SalesManagement.Repositories.Interfaces;
using Backend.SalesManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.SalesManagement.Services
{
    public class UserService : IUserService
    {
        private IDictionary<string, (string passWord, User user)> _users =
            new Dictionary<string, (string passWord, User user)>();

        public UserService(IDictionary<string, string> credentials)
        {
            foreach (var cred in credentials)
            {
                _users.Add(cred.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(cred.Value), new User(cred.Key)));
            }
        }
        public Task<bool> ValidateCredentials(string userName, string passWord, out User user)
        {
            user = null;
            var key = userName.ToLower();

            if (_users.ContainsKey(key))
            {
                if (BCrypt.Net.BCrypt.Verify(passWord, _users[key].passWord))
                {
                    user = _users[key].user;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
