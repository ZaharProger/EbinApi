using EbinApi.Contexts;
using EbinApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services.Strategy
{
    public class DownloadAppStrategy(long appId) : AppsBuilderStrategy
    {
        private readonly long _appId = appId;

        public override IQueryable<App> Build(EbinContext context)
        {
            return base.Build(context)
                .Where(app => app.Id == _appId)
                .Include(app => app.Updates)
                .Select(app => new App()
                {
                    LastUpdate = app.Updates.Count != 0?
                        app.Updates.OrderBy(update => -update.Date).First() :
                        null,
                });
        }
    }
}