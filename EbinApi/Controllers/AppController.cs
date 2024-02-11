using System.ComponentModel.DataAnnotations;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using EbinApi.Models.Http;
using EbinApi.Services;
using EbinApi.Services.Strategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EbinApi.Controllers
{
    [ApiController]
    [Route("api/apps")]
    [Authorize]
    public class AppController(AppService appService, UserService userService): ControllerBase
    {
        private readonly AppService _appService = appService;
        private readonly UserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetApps([FromQuery] AppParams appParams)
        {
            var authorizedUser = HttpContext.User.Claims
                .Where(claim => claim.Type == "user_id")
                .ToArray();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (!authorizedUser.IsNullOrEmpty())
            {
                var userId = long.Parse(authorizedUser[0].Value);
                var foundUser = await _userService.GetUserById(userId);

                if (foundUser != null)
                {
                    var userRole = foundUser.Role.Name.ToLower();
                    List<App> apps = [];

                    if (userRole.Equals(UserRoles.ADMIN.GetStringValue().ToLower()))
                    {
                        apps = await _appService.GetApps(new AdminAppsBuilderStrategy());
                    }
                    else if (userRole.Equals(UserRoles.USER.GetStringValue().ToLower()))
                    {
                        AppsBuilderStrategy strategy = appParams.IsInstalled ? 
                            new UserAppsBuilderStrategy(foundUser) :
                            new CompanyAppsBuilderStrategy(foundUser, appParams.IsTest);
                        apps = await _appService.GetApps(strategy);
                    }

                    response = new JsonResult(new CollectionResponse<App>()
                    {
                        Message = "",
                        Objects = apps
                    });
                }
            }

            return response;
        }

        [HttpDelete]
        [Route("uninstall")]
        public async Task<IActionResult> UninstallApp([FromQuery] [Required] long appId)
        {
            var authorizedUser = HttpContext.User.Claims
                .Where(claim => claim.Type == "user_id")
                .ToArray();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (!authorizedUser.IsNullOrEmpty())
            {
                var userId = long.Parse(authorizedUser[0].Value);
                var foundUser = await _userService.GetUserById(userId);
                
                if (foundUser != null && foundUser.Role.Name == UserRoles.USER.GetStringValue())
                {
                    await _appService.UninstallAppById(userId, appId);

                    response = Ok(new BaseResponse()
                    {
                        Message = ""
                    });
                }
            }

            return response;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApp([FromQuery] [Required] long appId)
        {
            var authorizedUser = HttpContext.User.Claims
                .Where(claim => claim.Type == "user_id")
                .ToArray();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (!authorizedUser.IsNullOrEmpty())
            {
                var userId = long.Parse(authorizedUser[0].Value);
                var foundUser = await _userService.GetUserById(userId);
                
                if (foundUser != null && foundUser.Role.Name == UserRoles.ADMIN.GetStringValue())
                {
                    await _appService.DeleteAppById(appId);

                    response = Ok(new BaseResponse()
                    {
                        Message = ""
                    });
                }
            }

            return response;
        }
    }
}