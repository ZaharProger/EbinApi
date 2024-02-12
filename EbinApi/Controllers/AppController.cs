using System.ComponentModel.DataAnnotations;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using EbinApi.Models.Http;
using EbinApi.Services;
using EbinApi.Services.Strategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbinApi.Controllers
{
    [ApiController]
    [Route("api/apps")]
    [Authorize]
    public class AppController(AppService appService, UserService userService)
    : EbinController(userService)
    {
        private readonly AppService _appService = appService;

        [HttpGet]
        public async Task<IActionResult> GetApps([FromQuery] AppParams appParams)
        {
            var authorizedUser = await CheckSession();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser != null)
            {
                var userRole = authorizedUser.Role.Name.ToLower();
                List<App> apps = [];

                if (userRole.Equals(UserRoles.ADMIN.GetStringValue().ToLower()))
                {
                    apps = await _appService.GetApps(new AdminAppsBuilderStrategy());
                }
                else if (userRole.Equals(UserRoles.USER.GetStringValue().ToLower()))
                {
                    AppsBuilderStrategy strategy = appParams.IsInstalled ?
                        new UserAppsBuilderStrategy(authorizedUser) :
                        new CompanyAppsBuilderStrategy(authorizedUser, appParams.IsTest);
                    apps = await _appService.GetApps(strategy);
                }

                response = new JsonResult(new CollectionResponse<App>()
                {
                    Message = "",
                    Objects = apps
                });
            }

            return response;
        }

        [HttpGet]
        [Route("{appId}")]
        public async Task<IActionResult> GetApp([FromRoute] long appId)
        {
            var authorizedUser = await CheckSession();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser != null)
            {
                var userRole = authorizedUser.Role.Name.ToLower();
                App? app = null;

                if (userRole.Equals(UserRoles.ADMIN.GetStringValue().ToLower()))
                {
                    app = await _appService.GetApp(
                        new AdminAppCardBuilderStrategy(appId)
                    );
                }
                else if (userRole.Equals(UserRoles.USER.GetStringValue().ToLower()))
                {
                    app = await _appService.GetApp(
                        new UserAppCardBuilderStrategy(authorizedUser, appId)
                    );
                }

                if (app != null)
                {
                    response = new JsonResult(new SingleObjectResponse<App>()
                    {
                        Message = "",
                        Object = app
                    });
                }
                else
                {
                    response = BadRequest(new BaseResponse()
                    {
                        Message = "Приложение не найдено либо удалено разработчиком!",
                    });
                }
            }

            return response;
        }

        [HttpDelete]
        [Route("uninstall")]
        public async Task<IActionResult> UninstallApp([FromQuery][Required] long appId)
        {
            var authorizedUser = await CheckSession();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser != null)
            {
                var userRole = authorizedUser.Role.Name.ToLower();
                if (userRole.Equals(UserRoles.USER.GetStringValue().ToLower()))
                {
                    await _appService.UninstallAppById(authorizedUser.Id, appId);

                    response = Ok(new BaseResponse()
                    {
                        Message = ""
                    });
                }
            }

            return response;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApp([FromQuery][Required] long appId)
        {
            var authorizedUser = await CheckSession();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser != null)
            {
                var userRole = authorizedUser.Role.Name.ToLower();
                if (userRole.Equals(UserRoles.ADMIN.GetStringValue().ToLower()))
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