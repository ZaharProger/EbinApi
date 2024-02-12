using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class AdminAppCardBuilderStrategy(long appId): AppsBuilderStrategy
    {
        private readonly long _appId = appId;

        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Where(app => app.Id == _appId)
                .Include(app => app.Updates)
                .Include(app => app.Reviews)
                .ThenInclude(review => review.User)
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Status = app.Status,
                    Icon = app.Icon,
                    Description = app.Description,
                    Developer = app.Developer,
                    Images = app.Images,
                    MinIos = app.MinIos,
                    MinAndroid = app.MinAndroid,
                    Size = app.Updates.Count != 0?
                        new FileInfo(
                            app.Updates
                                .OrderBy(update => -update.Date)
                                .Last()
                                .FilePath
                        )
                        .Length
                        .FormatSize() :
                        null,
                    LastUpdate = app.Updates.Count != 0?
                        app.Updates.OrderBy(update => -update.Date).First() :
                        null,
                    Downloads = app.Users.Count,
                    Rating = app.Reviews.Count == 0?
                        0.0F :
                        app.Reviews.Sum(review => review.Rating) / app.Reviews.Count,
                    Updates = app.Updates,
                    Reviews = app.Reviews
                });
        }
    }
}