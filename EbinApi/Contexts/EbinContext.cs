using EbinApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Contexts 
{
    public class EbinContext(DbContextOptions<EbinContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserApp> UserApps { get; set; }
        public DbSet<AuthCode> AuthCodes { get; set; }
        public DbSet<Role> Roles { get; set; }
    }   
}