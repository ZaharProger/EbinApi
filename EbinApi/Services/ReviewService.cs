using EbinApi.Contexts;
using EbinApi.Models.Db;
using EbinApi.Models.Http;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    public class ReviewService(EbinContext context)
    {
        private readonly EbinContext _context = context;

        public async Task<List<Review>> GetAppReviews(long appId)
        {
            return await _context.Reviews
                .Where(review => review.AppId == appId)
                .ToListAsync();
        }

        public async Task<bool> SendReview(SendReviewParams reviewData, User user)
        {
            bool isSuccessful;
            try
            {
                var newReview = new Review()
                {
                    App = await _context.Apps.FindAsync(reviewData.AppId),
                    Description = reviewData.Description,
                    Rating = reviewData.Rating,
                    IsViewed = false,
                    Date = DateTimeOffset.UtcNow.ToUnixTimeSeconds() * 1000,
                    User = await _context.Users.FindAsync(user.Id)
                };
                await _context.Reviews.AddAsync(newReview);

                await _context.SaveChangesAsync();
                isSuccessful = true;
            }
            catch (Exception)
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }
    }
}