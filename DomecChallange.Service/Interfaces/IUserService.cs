using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;

namespace DomecChallange.Service.Interfaces
{
    public interface IUserService
    {
        Task<StatusDto<User>> ValidateCredentials(string userName, string password, out User user);  
    }
}
