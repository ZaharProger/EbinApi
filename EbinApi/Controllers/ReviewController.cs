using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using EbinApi.Models.Http;
using EbinApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbinApi.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ReviewController(ReviewService reviewService, UserService userService)
    : EbinController(userService)
    {
        private readonly ReviewService _reviewService = reviewService;

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] long appId)
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
                    var reviews = await _reviewService.GetAppReviews(appId);
                    response = new JsonResult(new CollectionResponse<Review>()
                    {
                        Message = "",
                        Objects = reviews
                    });
                }
            }

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> SendReview([FromForm] SendReviewParams reviewData)
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
                    var isSuccessful = await _reviewService.SendReview(
                        reviewData, 
                        authorizedUser
                    );
                    
                    if (isSuccessful)
                    {
                        response = Ok(new BaseResponse()
                        {
                            Message = ""
                        });
                    }
                    else
                    {
                        response = BadRequest(new BaseResponse()
                        {
                            Message = "Произошла ошибка при отправке отзыва!"
                        });
                    }
                }
            }

            return response;
        }
    }
}