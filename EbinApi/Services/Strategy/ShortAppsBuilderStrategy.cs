using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;

namespace EbinApi.Services.Strategy
{
    public class ShortAppsBuilderStrategy: AppsBuilderStrategy
    {
        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Select(app => new App()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Icon = app.Icon,
                    Developer = app.Developer
                });
        }
    }
}