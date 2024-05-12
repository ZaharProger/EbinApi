using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class AdminAppCardBuilderStrategy(long appId) : AppsBuilderStrategy
    {
        private readonly long _appId = appId;

        public override IQueryable<App> Build(EbinContext context)
        {
            Func<List<Update>, string?> calcSizeFunc = (updates) =>
            {
                string? lastUpdateFilePath = null;

                if (updates.Count != 0)
                {
                    lastUpdateFilePath = updates
                        .OrderBy(update => -update.Date)
                        .First()
                        .FilePath;
                }

                return lastUpdateFilePath != null? 
                    new FileInfo(lastUpdateFilePath).Length.FormatSize()
                    :
                    null;
            };

            return base.Build(context)
                .Where(app => app.Id == _appId)
                .Include(app => app.Reviews)
                .ThenInclude(review => review.User)
                .Include(app => app.Updates)
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Status = app.Status,
                    Icon = app.Icon,
                    Access = app.Access,
                    Description = app.Description,
                    Developer = app.Developer,
                    Images = app.Images,
                    MinIos = app.MinIos,
                    MinAndroid = app.MinAndroid,
                    Size = calcSizeFunc(app.Updates),
                    LastUpdate = app.Updates.Count != 0 ?
                        app.Updates.OrderBy(update => -update.Date).First() :
                        null,
                    Downloads = app.Users.Count,
                    Rating = app.Reviews.Count == 0 ?
                        0.0F :
                        app.Reviews.Sum(review => review.Rating) / app.Reviews.Count,
                    Updates = app.Updates,
                    Reviews = app.Reviews,
                    Companies = app.Companies,
                });
        }
    }
}