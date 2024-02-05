using System.Text.Json.Serialization;
using EbinApi.Contexts;
using EbinApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace EbinApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("PostgresConnection");

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddAuthorization();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "session_id";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromDays(14);
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "session_id";
                    options.ExpireTimeSpan = TimeSpan.FromDays(14);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                });

            builder.Services.AddDbContext<EbinContext>(options =>
            {
                options.UseNpgsql(connection);
            });

            builder.Services.AddTransient<UserService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllers();

            app.Run();
        }
    }
}