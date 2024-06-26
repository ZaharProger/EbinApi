using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using EbinApi.Models.Db;
using EbinApi.Models.Http;
using EbinApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbinApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(UserService service) : EbinController(service)
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("code")]
        public async Task<IActionResult> SendCode([FromBody][Required] string phone)
        {
            IActionResult response;

            try
            {
                var generatedCode = await _userService.GenerateCode(phone);
                response = Ok(new BaseResponse()
                {
                    Message = generatedCode
                });
            }
            catch (Exception)
            {
                response = BadRequest(new BaseResponse()
                {
                    Message = "Ошибка при отправке кода на номер телефона. Попробуйте еще раз"
                });
            }

            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("auth")]
        public async Task<IActionResult> AuthorizeUser([FromBody] UserAuthorizeData userData)
        {
            IActionResult response;
            var authorizedUser = await _userService.AuthorizeUser(userData);

            if (authorizedUser != null)
            {
                var claims = new List<Claim>()
                {
                    new("user_id", authorizedUser.Id.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                response = Ok(new BaseResponse()
                {
                    Message = ""
                });

            }
            else
            {
                response = BadRequest(new BaseResponse()
                {
                    Message = "Введен неверный код!"
                });
            }

            return response;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInfo()
        {
            var authorizedUser = await CheckSession();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser != null)
            {
                authorizedUser.Phone = "";
                authorizedUser.LastName = "";
                response = new JsonResult(new SingleObjectResponse<User>()
                {
                    Message = "",
                    Object = authorizedUser
                });
            }

            return response;
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}