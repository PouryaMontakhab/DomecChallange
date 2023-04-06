using DomecChallange.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> SignIn()
        {

        }
        #endregion
    }
}
