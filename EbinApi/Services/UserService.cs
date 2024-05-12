using EbinApi.Contexts;
using EbinApi.Extensions;
using EbinApi.Models.Db;
using EbinApi.Models.Enums;
using EbinApi.Models.Http;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    public class UserService(EbinContext context)
    {
        private readonly EbinContext _context = context;
        private static readonly Random _random = new();

        public async Task<string> GenerateCode(string phone)
        {
            if (await _context.Users.FindAsync(1L) == null)
            {
                Role roleAdmin = new() { Name = UserRoles.ADMIN.GetStringValue() };
                Role roleUser = new() { Name = UserRoles.USER.GetStringValue() };
                Company company = new() { Name = "Bulochka" };

                User admin = new()
                {
                    Name = "Александр",
                    LastName = "Потапов",
                    Status = "Инженер контрольно-измерительных приборов",
                    Phone = "+79149594112",
                    Role = roleAdmin,
                    Company = company,
                };
                User zahar = new()
                {
                    Name = "Захар",
                    LastName = "Домолего",
                    Status = "Web developer",
                    Phone = "+79041220625",
                    Role = roleUser,
                    Company = company,
                };

                await _context.Roles.AddAsync(roleAdmin);
                await _context.Roles.AddAsync(roleUser);
                await _context.Companies.AddAsync(company);

                await _context.Users.AddAsync(admin);
                await _context.Users.AddAsync(zahar);

                Account accountZahar = new() { User = zahar };
                Account accountAdmin = new() { User = admin };

                await _context.Accounts.AddAsync(accountAdmin);
                await _context.Accounts.AddAsync(accountZahar);

                await _context.SaveChangesAsync();
            }

            var newCode = _random.Next(10000).ToString("D4");
            var newCodePhonePair = new AuthCode()
            {
                Phone = phone,
                Code = newCode
            };

            await _context.AuthCodes.AddAsync(newCodePhonePair);
            await _context.SaveChangesAsync();

            return newCode;
        }

        public async Task<User?> AuthorizeUser(UserAuthorizeData userData)
        {
            var foundData = await _context.AuthCodes
                .Where(authCode => authCode.Phone.Equals(userData.Phone) &&
                    authCode.Code.Equals(userData.Code))
                .ToArrayAsync();
            User? authorizedUser = null;

            if (foundData.Length != 0)
            {
                var foundUser = await GetUserByPhone(userData.Phone);
                if (foundUser != null)
                {
                    _context.AuthCodes.Remove(foundData[0]);
                    await _context.SaveChangesAsync();
                    authorizedUser = foundUser;
                }
            }

            return authorizedUser;
        }

        public async Task<User?> GetUserById(long id)
        {
            var foundUser = await _context.Users
                .Where(user => user.Id == id)
                .Include(user => user.Account)
                .Include(user => user.Company)
                .Include(user => user.Role)
                .Select(user => new User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Status = user.Status,
                    CompanyId = user.CompanyId,
                    Company = new()
                    {
                        Name = user.Company.Name
                    },
                    Account = new()
                    {
                        DarkTheme = user.Account.DarkTheme,
                        PushInstall = user.Account.PushInstall,
                        PushUpdate = user.Account.PushUpdate,
                        UserId = user.Id
                    },
                    Role = user.Role,
                    RoleId = user.RoleId
                })
                .ToArrayAsync();

            return foundUser.Length != 0 ? foundUser[0] : null;
        }

        public async Task<User?> GetUserByPhone(string phone)
        {
            var foundUser = await _context.Users
                .Where(user => user.Phone == phone)
                .ToArrayAsync();

            return foundUser.Length != 0 ? foundUser[0] : null;
        }
    }
}