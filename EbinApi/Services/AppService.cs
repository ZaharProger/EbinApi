using EbinApi.Contexts;
using EbinApi.Models.Db;
using EbinApi.Models.Http;
using EbinApi.Services.Strategy;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    public class AppService(EbinContext context)
    {
        private readonly EbinContext _context = context;
        private readonly string apkBasePath = "Repository/apk/";
        private readonly string iconBasePath = "Repository/icons/";
        private readonly string imagesBasePath = "Repository/images/";

        public async Task<List<App>> GetApps(AppsBuilderStrategy strategy)
        {
            return await strategy.Build(_context).ToListAsync();
        }

        public async Task<App?> GetApp(AppsBuilderStrategy strategy)
        {
            var foundApp = await strategy.Build(_context).ToArrayAsync();
            return foundApp.Length == 0 ? null : foundApp[0];
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

        public async Task<bool> PublishApp(PublishAppData appData)
        {
            var isSuccessful = true;

            try
            {
                string? iconFilePath = null;
                if (appData.IconFile != null)
                {
                    var imageId = Guid.NewGuid().ToString();
                    iconFilePath = $"{iconBasePath}{imageId}_{appData.IconFile.FileName}";
                    using (Stream stream = new FileStream(iconFilePath, FileMode.Create))
                    {
                        appData.IconFile.CopyTo(stream);
                    };
                }

                List<string> imagesFilePaths = new();
                if (appData.ImagesFiles.Count != 0)
                {
                    foreach (var imageFile in appData.ImagesFiles)
                    {
                        var imageId = Guid.NewGuid().ToString();
                        var imageFilePath = $"{imagesBasePath}{imageId}_{imageFile.FileName}";
                        using (Stream stream = new FileStream(imageFilePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        };
                        imagesFilePaths.Add(imageFilePath);
                    }
                }

                var newApp = new App()
                {
                    Name = appData.Name,
                    Status = appData.Status,
                    Developer = appData.Developer,
                    Description = appData.Description,
                    MinAndroid = appData.MinAndroid,
                    MinIos = appData.MinIos,
                    Icon = iconFilePath,
                    Images = imagesFilePaths.Count != 0 ?
                        string.Join("\n", imagesFilePaths) :
                        null
                };

                await _context.Apps.AddAsync(newApp);

                string? apkFilePath = null;
                if (appData.ApkFile != null)
                {
                    var apkId = Guid.NewGuid().ToString();
                    apkFilePath = $"{apkBasePath}{apkId}_{appData.ApkFile.FileName}";
                    using (Stream stream = new FileStream(apkFilePath, FileMode.Create))
                    {
                        appData.ApkFile.CopyTo(stream);
                    };
                }

                newApp.Updates.Add(new Update()
                {
                    Version = appData.Version,
                    Date = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Description = appData.Description,
                    TestFlight = appData.TestFlight,
                    FilePath = apkFilePath
                });

                var splittedCompanies = appData.Companies
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();
                var acceptedCompanies = await _context.Companies
                    .Where(company => splittedCompanies.Contains(company.Id))
                    .ToArrayAsync();
                newApp.Companies.AddRange(acceptedCompanies);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }

        public async Task<bool> UpdateApp(UpdateData updateData)
        {
            var isSuccessful = true;

            try
            {
                var foundApp = await _context.Apps
                    .Where(app => app.Id == updateData.AppId)
                    .ToArrayAsync();

                if (foundApp.Length != 0)
                {
                    string? apkFilePath = null;
                    if (updateData.ApkFile != null)
                    {
                        var apkId = Guid.NewGuid().ToString();
                        apkFilePath = $"{apkBasePath}{apkId}_{updateData.ApkFile.FileName}";
                        using (Stream stream = new FileStream(apkFilePath, FileMode.Create))
                        {
                            updateData.ApkFile.CopyTo(stream);
                        };
                    }

                    await _context.Updates.AddAsync(new Update()
                    {
                        Version = updateData.Version,
                        Description = updateData.Description,
                        Date = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        TestFlight = updateData.TestFlight,
                        FilePath = apkFilePath,
                        App = foundApp[0]
                    });

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }

        public async Task<bool> EditApp(EditAppData appData)
        {
            var isSuccessful = true;

            try
            {
                string? iconFilePath = null;
                if (appData.IconFile != null)
                {
                    var imageId = Guid.NewGuid().ToString();
                    iconFilePath = $"{iconBasePath}{imageId}_{appData.IconFile.FileName}";
                    using (Stream stream = new FileStream(iconFilePath, FileMode.Create))
                    {
                        appData.IconFile.CopyTo(stream);
                    };
                }

                List<string> imagesFilePaths = new();
                if (appData.ImagesFiles.Count != 0)
                {
                    foreach (var imageFile in appData.ImagesFiles)
                    {
                        var imageId = Guid.NewGuid().ToString();
                        var imageFilePath = $"{imagesBasePath}{imageId}_{imageFile.FileName}";
                        using (Stream stream = new FileStream(imageFilePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        };
                        imagesFilePaths.Add(imageFilePath);
                    }
                }

                var foundApp = await _context.Apps.FindAsync(long.Parse(appData.Id));
                if (foundApp != null)
                {
                    foundApp.Name = appData.Name;
                    foundApp.Status = appData.Status;
                    foundApp.Developer = appData.Developer;
                    foundApp.Description = appData.Description;
                    foundApp.MinAndroid = appData.MinAndroid;
                    foundApp.MinIos = appData.MinIos;
                    foundApp.Icon = iconFilePath;
                    foundApp.Images = imagesFilePaths.Count != 0 ?
                        string.Join("\n", imagesFilePaths) :
                        null;

                    string? apkFilePath = null;
                    if (appData.ApkFile != null)
                    {
                        var apkId = Guid.NewGuid().ToString();
                        apkFilePath = $"{apkBasePath}{apkId}_{appData.ApkFile.FileName}";
                        using (Stream stream = new FileStream(apkFilePath, FileMode.Create))
                        {
                            appData.ApkFile.CopyTo(stream);
                        };
                    }

                    var foundAppUpdates = await _context.Updates
                        .Where(update => update.AppId == foundApp.Id)
                        .OrderBy(update => -update.Id)
                        .ToArrayAsync();
                    if (foundAppUpdates.Length != 0)
                    {
                        foundAppUpdates[0].Version = appData.Version;
                        foundAppUpdates[0].Description = appData.Description;
                        foundAppUpdates[0].TestFlight = appData.TestFlight;
                        foundAppUpdates[0].FilePath = apkFilePath;
                    }

                    var splittedCompanies = appData.Companies
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(long.Parse)
                            .ToArray();
                    var acceptedCompanies = await _context.Companies
                        .Where(company => splittedCompanies.Contains(company.Id))
                        .ToArrayAsync();
                    
                    foundApp.Companies.Clear();
                    foundApp.Companies.AddRange(acceptedCompanies);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }
    }
}