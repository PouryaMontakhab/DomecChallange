using DomecChallange.Data.Context;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Dtos.Enums;
using DomecChallange.Dtos.ProdcutDtos;
using DomecChallange.Service.Interfaces;
using DomecChallange.Service.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DomecChallange.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly Dictionary<string, string> validUser;
        public UserServiceTest()
        {
            validUser = new Dictionary<string, string> { { "mirco", "mirco123" } };
        }

        [TestMethod]
        public async Task ValidateCredentials_WhenCallWithValidCredential_ShouldReturnSuccessStatus()
        {
            User user;
            var userService = new UserService(validUser);

            var result = await userService.ValidateCredentials("mirco", "mirco123", out user);

            Assert.IsInstanceOfType(result,typeof(StatusDto<User>));
            Assert.AreEqual(result.Status, StatusEnum.Success);
        }
        [TestMethod]
        public async Task ValidateCredentials_WhenCallWithInValidCredential_ShouldReturnErrorStatus()
        {
            User user;
            var userService = new UserService(validUser);

            var result = await userService.ValidateCredentials("invalidUserName", "invalidPassword", out user);

            Assert.IsInstanceOfType(result, typeof(StatusDto<User>));
            Assert.AreEqual(result.Status, StatusEnum.Error);
        }
    }

}