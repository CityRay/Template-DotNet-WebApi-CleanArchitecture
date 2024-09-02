using System.Security.Claims;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.ModelContract.WebAPI.Request;
using CleanArchitecture.ModelContract.WebAPI.Response;
using CleanArchitecture.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "登入", Description = "使用者登入")]
        [SwaggerResponse(200, "登入成功", typeof(LoginResponse))]
        [SwaggerResponse(401, "登入失敗")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized("Invalid");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="registerDto">註冊請求資料</param>
        /// <returns>註冊回應資料</returns>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "註冊", Description = "使用者註冊")]
        [SwaggerResponse(200, "註冊成功", typeof(LoginResponse))]
        [SwaggerResponse(400, "註冊失敗")]
        public async Task<ActionResult<LoginResponse>> Register(RegisterRequest registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return BadRequest("Email taken");
            }

            if (await _userManager.FindByNameAsync(registerDto.Username) != null)
            {
                return BadRequest("Username taken");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            _logger.LogError($"Error creating user: {result.Errors}");
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// 取得使用者
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "取得使用者", Description = "取得當前登入的使用者資料")]
        [SwaggerResponse(200, "取得成功", typeof(LoginResponse))]
        [SwaggerResponse(401, "未授權")]
        public async Task<ActionResult<LoginResponse>> GetCurrentUser()
        {
            var id = ClaimTypes.NameIdentifier;
            if (string.IsNullOrWhiteSpace(id))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(User.FindFirstValue(id));

            if (user == null) return Unauthorized();

            return CreateUserObject(user);
        }

        private LoginResponse CreateUserObject(AppUser user)
        {
            return new LoginResponse
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
                Image = ""
            };
        }
    }
}
