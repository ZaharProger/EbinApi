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
    public class UserController(UserService service): ControllerBase
    {
        private readonly UserService _service = service;

        [HttpPost]
        [AllowAnonymous]
        [Route("code")]
        public async Task<IActionResult> SendCode([FromForm][Required] string phone)
        {
            IActionResult response;

            try
            {
                await _service.GenerateCode(phone);
                response = Ok(new BaseResponse()
                {
                    Message = ""
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
        public async Task<IActionResult> AuthorizeUser([FromForm] UserAuthorizeData userData)
        {
            IActionResult response;
            var authorizedUser = await _service.AuthorizeUser(userData);

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
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var authorizedUser = HttpContext.User.Claims
                .Where(claim => claim.Type == "user_id")
                .ToArray();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser.Length != 0)
            {
                var userId = long.Parse(authorizedUser[0].Value);
                var foundUser = await _service.GetUserById(userId);

                if (foundUser != null)
                {
                    response = new JsonResult(new SingleObjectResponse<User>()
                    {
                        Message = "",
                        Object = new User()
                        {
                            Id = foundUser.Id,
                            Name = foundUser.Name,
                            Status = foundUser.Status,
                            CompanyId = foundUser.CompanyId,
                            Company = new()
                            {
                                Name = foundUser.Company.Name
                            },
                            Account = new()
                            {
                                DarkTheme = foundUser.Account.DarkTheme,
                                PushInstall = foundUser.Account.PushInstall,
                                PushUpdate = foundUser.Account.PushUpdate,
                                UserId = foundUser.Id
                            }
                        }
                    });
                }
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