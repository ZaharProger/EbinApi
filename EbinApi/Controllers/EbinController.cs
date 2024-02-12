using EbinApi.Models.Db;
using EbinApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EbinApi.Controllers
{
    public class EbinController(UserService service): ControllerBase
    {
        protected readonly UserService _userService = service;

        protected async Task<User?> CheckSession()
        {
            var authorizedUser = HttpContext.User.Claims
                .Where(claim => claim.Type == "user_id")
                .ToArray();
            User? foundUser = null;

            if (authorizedUser.Length != 0)
            {
                var userId = long.Parse(authorizedUser[0].Value);
                foundUser = await _userService.GetUserById(userId);
            }

            return foundUser;
        }
    }
}