using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapShop.APIs.DTOs;
using SnapShop.APIs.Errors;
using SnapShop.Core.Models.Identity;

namespace SnapShop.APIs.Controllers
{

    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(UserManager<AppUser> manager, SignInManager<AppUser> signInManager)
        {
            _manager = manager;
            _signInManager = signInManager;
        }

        //Regester

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Regester(RegisterAttributeDTO model)
        {
            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };

            var result = await _manager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            } else
            {
                var returnedUser = new UserDTO
                {
                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    Token = "this is token"
                };
                return Ok(returnedUser);
            }
        }

        //LogIn  
        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto model) 
        {
            var user =await _manager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var returenedUser = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "this is token"
            };
            return Ok(returenedUser);

        }
}}
