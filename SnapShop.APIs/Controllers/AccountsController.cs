using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapShop.APIs.DTOs;
using SnapShop.APIs.Errors;
using SnapShop.Core.Models.Identity;
using SnapShop.Core.Services;
using System.Security.Claims;

namespace SnapShop.APIs.Controllers
{

    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> manager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _manager = manager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        //Regester

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Regester(RegisterAttributeDTO model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
            }

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
                    Token = await _tokenService.CreateTokenAsync(user, _manager)
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
                Token = await _tokenService.CreateTokenAsync(user, _manager)
            };
            return Ok(returenedUser);

        }


        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCuttentUser() 
        {
           var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _manager.FindByEmailAsync(email);

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = email,
                Token = await _tokenService.CreateTokenAsync(user , _manager)
            });
        
        }

        [HttpGet("checkEmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email) 
        {
        return await _manager.FindByEmailAsync(email) is not null;
        }
}}
