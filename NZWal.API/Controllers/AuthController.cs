using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWal.API.Models.DTO;
using NZWal.API.Repositories;

namespace NZWal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        #region Register
        // POST : /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            //Tạo đối tượng mới
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            //Tạo người dùng trong hệ thống
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            //Kiểm tra và gán vai trò cho người dùng
            if (identityResult.Succeeded)
            {
                //Add roles to this user
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    
                    if(identityResult.Succeeded)
                    {
                        return Ok("Người dùng đã được đăng kí, Vui lòng đăng nhập!");
                    }
                }
            }

            return BadRequest("Đã xảy ra sự cố!");
        }
        #endregion

        #region Login
        // POST : /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if(user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(checkPasswordResult)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null && roles.Any())
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResposeDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }

                    return Ok();
                }
            }

            return BadRequest("Email hoặc mật khẩu không hợp lệ, Vui lòng kiếm tra lại!");
        }
        #endregion
    }
}
