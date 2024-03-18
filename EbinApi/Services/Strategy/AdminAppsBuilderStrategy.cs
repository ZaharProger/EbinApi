using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class AdminAppsBuilderStrategy: AppsBuilderStrategy
    {
        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Include(app => app.Updates)
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Icon = app.Icon,
                    Developer = app.Developer,
                    Access = app.Users.Count == 0? 
                        AppAccesses.CLOSE.GetStringValue() : 
                        AppAccesses.OPEN.GetStringValue(),
                    Status = app.Status,
                    Downloads = app.Users.Count,
                    LastUpdate = app.Updates.Count != 0?
                        app.Updates.OrderBy(update => -update.Date).First() :
                        null,
                    Rating = app.Reviews.Count == 0?
                        0.0F :
                        app.Reviews.Sum(review => review.Rating) / app.Reviews.Count,
                    Updates = app.Updates
                });
        }
    }
}