using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using Microsoft.IdentityModel.Tokens;

namespace EbinApi.Services.Strategy
{
    public class UserAppsBuilderStrategy(User user) : AppsBuilderStrategy
    {
        private readonly User _user = user;

        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Where(app => app.Users
                    .Any(appUser => appUser.Id == _user.Id))
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Icon = app.Icon,
                    Size = !app.Updates.IsNullOrEmpty()?
                        new FileInfo(
                            app.Updates
                                .OrderBy(update => -update.Date)
                                .Last()
                                .FilePath
                        )
                        .Length
                        .FormatSize() :
                        null
                });
        }
    }
}