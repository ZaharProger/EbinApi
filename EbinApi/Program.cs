using System.Text.Json.Serialization;
using EbinApi.Contexts;
using EbinApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

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

            builder.Services.AddCors();

            builder.Services.AddAuthorization();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "session_id";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromDays(14);
                options.Cookie.SameSite = SameSiteMode.None;
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
                    options.Cookie.SameSite = SameSiteMode.None;
                });

            builder.Services.AddDbContext<EbinContext>(options =>
            {
                options.UseNpgsql(connection);
            });

            builder.Services.AddTransient<UserService>();
            builder.Services.AddTransient<AppService>();
            builder.Services.AddTransient<CompanyService>();
            builder.Services.AddTransient<ReviewService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            FileExtensionContentTypeProvider contentTypes = new();
            contentTypes.Mappings[".apk"] = "application/vnd.android.package-archive";
            contentTypes.Mappings[".png"] = "image/png";
            contentTypes.Mappings[".jpg"] = "image/jpg";
            contentTypes.Mappings[".jpeg"] = "image/jpeg";
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllers();

            app.UseCors(x => x
                .WithOrigins("http://77.222.60.86:8080", "http://localhost:3000")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Repository")
                ),
                RequestPath = "/wwwroot/Repository",
                ContentTypeProvider = contentTypes,
                OnPrepareResponse = (context) =>
                {
                    context.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    context.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                    context.Context.Response.Headers.Append("Access-Control-Expose-Headers", "Content-Disposition, Content-Length, X-Content-Transfer-Id");
                    context.Context.Response.Headers.Append("Content-Disposition", "inline");
                    context.Context.Response.Headers.Append("X-Content-Transfer-Id", "12345");
                }
            });

            app.Run();
        }
    }
}