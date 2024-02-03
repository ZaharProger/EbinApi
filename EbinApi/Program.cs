using EbinApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EbinApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("PostgresConnection");

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<EbinContext>(options => options.UseNpgsql(connection));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}