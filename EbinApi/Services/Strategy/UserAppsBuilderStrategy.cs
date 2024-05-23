using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class UserAppsBuilderStrategy(User user) : AppsBuilderStrategy
    {
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
                .Include(app => app.Users)
                .Where(app => app.UserApps
                        .Any(userApp => userApp.Id == _user.Id))
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Icon = app.Icon,
                    Size = calcSizeFunc(app.Updates),
                });
        }
    }
}