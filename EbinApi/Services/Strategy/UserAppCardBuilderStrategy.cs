using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class UserAppCardBuilderStrategy(User user, long appId): AppsBuilderStrategy
    {
        private readonly long _appId = appId;
        private readonly User _user = user;

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
                .Where(app => app.Id == _appId && 
                    (app.Access == AppAccesses.OPEN.GetStringValue() || 
                    (app.Access == AppAccesses.PARTIAL.GetStringValue() && app.Companies
                        .Any(appCompany => appCompany.Id == user.CompanyId))))
                .Include(app => app.Updates)
                .Include(app => app.Users)
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
                    Size = calcSizeFunc(app.Updates),
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