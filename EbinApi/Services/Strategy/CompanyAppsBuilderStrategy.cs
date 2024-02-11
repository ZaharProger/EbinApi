using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using EbinApi.Services.Strategy;
using Microsoft.IdentityModel.Tokens;

namespace EbinApi.Services
{
    class CompanyAppsBuilderStrategy(User user, bool isTest): AppsBuilderStrategy
    {
        private readonly User _user = user;
        private readonly bool _isTest = isTest;

        public override IQueryable<App> Build(EbinContext context)
        {
            var appsQuery = base.Build(context)
                .Where(app => app.Companies
                    .Any(appCompany => appCompany.Id == _user.CompanyId));
            
            if (_isTest)
            {
                appsQuery = appsQuery
                    .Where(app => app.Status.ToLower()
                        .Equals(AppStatuses.TEST.GetStringValue().ToLower()));  
            }

            return appsQuery
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
                        null,
                    IsInstalled = app.Users
                        .Any(userApp => userApp.Id == _user.Id)
                });
        }
    }
}