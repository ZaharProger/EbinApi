using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace EbinApi.Services.Strategy
{
    public class AdminAppsBuilderStrategy: AppsBuilderStrategy
    {
        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Icon = app.Icon,
                    Developer = app.Developer,
                    Access = app.Users.IsNullOrEmpty()? 
                        AppAccesses.CLOSE.GetStringValue() : 
                        AppAccesses.OPEN.GetStringValue(),
                    Status = app.Status,
                    Downloads = app.Users.Count,
                    LastUpdate = !app.Updates.IsNullOrEmpty()?
                        app.Updates.OrderBy(update => -update.Date).First() :
                        null,
                    Rating = app.Reviews.Count == 0?
                        0.0F :
                        app.Reviews.Sum(review => review.Rating) / app.Reviews.Count
                });
        }
    }
}