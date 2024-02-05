using EbinApi.Contexts;
using EbinApi.Models.Db;
using EbinApi.Models.Http;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    public class UserService(EbinContext context)
    {
        private readonly EbinContext _context = context;
        private static readonly Random _random = new();

        public async Task GenerateCode(string phone)
        {
            var newCodePhonePair = new AuthCode()
            {
                Phone = phone,
                Code = _random.Next(10000).ToString("D4")
            };

            await _context.AuthCodes.AddAsync(newCodePhonePair);
            await _context.SaveChangesAsync();
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
                .Include(user => user.Account)
                .Include(user => user.Company)
                .Where(user => user.Id == id)
                .ToArrayAsync();
            
            return foundUser.Length != 0? foundUser[0] : null;
        }

        public async Task<User?> GetUserByPhone(string phone)
        {
            var foundUser = await _context.Users
                .Where(user => user.Phone == phone)
                .ToArrayAsync();
            
            return foundUser.Length != 0? foundUser[0] : null;
        }
    }
}