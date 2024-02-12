using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class UserAppCardBuilderStrategy(User user, long appId): AppsBuilderStrategy
    {
        private readonly long _appId = appId;
        private readonly User _user = user;

        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Where(app => app.Id == _appId && app.Companies
                    .Any(appCompany => appCompany.Id == user.CompanyId))
                .Include(app => app.Updates)
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
                    Updates = app.Updates,
                    IsInstalled = app.Users
                        .Any(userApp => userApp.Id == _user.Id)
                });
        }
    }
}