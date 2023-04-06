using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Service.Interfaces;

namespace DomecChallange.Service.Services
{
    public class UserService : IUserService
    {
        #region Props
        private Dictionary<string, (string password, User user)> _user = new();
        #endregion
        #region Ctor
        public UserService(Dictionary<string, string> credentials)
            => credentials.ToList().ForEach(c => _user.Add(c.Key.ToLower(), (c.Value, new User(c.Value))));
        #endregion
        #region Methods
        public async Task<StatusDto<User>> ValidateCredentials(string userName, string password, out User user)
        {
            user = null;
            var key = userName.ToLower();
            if (_user.ContainsKey(key))
            {
                if(password == _user[key].password)
                {
                    user = _user[key].user;
                    return await Task.FromResult(new StatusDto<User> { Message="Signin was completed" });
                }
            }
            return await Task.FromResult(new StatusDto<User> { Message = "Signin was not completed" });
        }
        #endregion
    }
}
