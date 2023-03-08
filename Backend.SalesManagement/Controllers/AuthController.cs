using Backend.SalesManagement.Auth;
using Backend.SalesManagement.Models;
using Backend.SalesManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.SalesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        //ctor
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("signIn")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                User user;

                if (await _userService.ValidateCredentials(model.UserName, model.Password, out user))
                {
                    //Build Claims, get from database
                    var claims = new List<Claim>
                    {
                        //unique identifier for user
                        new Claim(ClaimTypes.NameIdentifier, model.UserName),
                        new Claim("name", model.UserName)
                    };

                    //build auth object
                    var authUser = TokenUtils.BuildUserAuthObject(user, claims);

                    return Ok(authUser);
                }
            }
            return BadRequest();
        }

    }
}
