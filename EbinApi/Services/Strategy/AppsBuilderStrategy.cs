using EbinApi.Contexts;
using EbinApi.Models.Db;

namespace EbinApi.Services.Strategy
{
    public class AppsBuilderStrategy
    {
        public virtual IQueryable<App> Build(EbinContext context)
        {
            return context.Apps.AsQueryable();
        }
    }
}