using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using EbinApi.Services.Strategy;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    class CompanyAppsBuilderStrategy(User user, bool isTest): AppsBuilderStrategy
    {
        private readonly User _user = user;
        private readonly bool _isTest = isTest;

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
            
            var appsQuery = base.Build(context)
                .Where(app => app.Access == AppAccesses.OPEN.GetStringValue() ||
                    (app.Access == AppAccesses.PARTIAL.GetStringValue() && 
                    app.Companies.Any(appCompany => appCompany.Id == _user.CompanyId)));
            
            if (_isTest)
            {
                appsQuery = appsQuery
                    .Where(app => app.Status.ToLower()
                        .Equals(AppStatuses.TEST.GetStringValue().ToLower()));  
            }

            return appsQuery
                .Include(app => app.UserApps)
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Icon = app.Icon,
                    Size = calcSizeFunc(app.Updates),
                    IsInstalled = app.UserApps
                        .Any(userApp => userApp.Id == _user.Id && userApp.AppId == app.Id)
                });
        }
    }
}