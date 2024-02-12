using EbinApi.Contexts;
using EbinApi.Models.Db;
using EbinApi.Services.Strategy;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    public class AppService(EbinContext context)
    {
        private readonly EbinContext _context = context;

        public async Task<List<App>> GetApps(AppsBuilderStrategy strategy)
        {
            return await strategy.Build(_context).ToListAsync();
        }

        public async Task<App?> GetApp(AppsBuilderStrategy strategy)
        {
            var foundApp = await strategy.Build(_context).ToArrayAsync();
            return foundApp.Length == 0? null : foundApp[0];
        }

        public async Task UninstallAppById(long userId, long appId)
        {
            var foundRelation = await _context.UserApps
                .Where(userApp => userApp.AppId == appId && userApp.UserId == userId)
                .ToArrayAsync();
            
            if (foundRelation.Length != 0)
            {
                _context.UserApps.Remove(foundRelation[0]);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAppById(long appId)
        {
            var foundApp = await _context.Apps
                .Where(app => app.Id == appId)
                .ToArrayAsync();
            
            if (foundApp.Length != 0)
            {
                _context.Apps.Remove(foundApp[0]);
                await _context.SaveChangesAsync();
            }
        }
    }
}