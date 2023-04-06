using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Dtos.Enums;
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
        public Task<StatusDto<User>> ValidateCredentials(string userName, string password, out User user)
        {
            user = null;
            var key = userName.ToLower();
            if (_user.ContainsKey(key))
            {
                if(password == _user[key].password)
                {
                    user = _user[key].user;
                    return Task.FromResult(new StatusDto<User> {Status = StatusEnum.Success, Message="Signin was completed" });
                }
            }
            return Task.FromResult(new StatusDto<User> { Status = StatusEnum.Error, Message = "Signin was not completed" });
        }
        #endregion
    }
}
