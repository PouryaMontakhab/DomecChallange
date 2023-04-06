using DomecChallange.Dtos.Enums;
using DomecChallange.Dtos.UserDtos;
using DomecChallange.Service.Interfaces;
using DomeChallange.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DomecChallange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        #region Props
        private readonly IUserService _userService;
        #endregion
        #region Ctor
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        #region Methods
        [HttpPost(Name =nameof(SignIn))]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            Domain.Entities.User user;
            var validationResult = await _userService.ValidateCredentials(model.UserName, model.Password, out user);
            if (validationResult.Status != StatusEnum.Success)  throw new Exception(validationResult.Message);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier , model.UserName),
                    new Claim("name",model.UserName)
                };
            var authUser = TokenUtils.BuildUserAuthObject(user, claims);
            return Ok(authUser);
        }
        #endregion
    }
}
