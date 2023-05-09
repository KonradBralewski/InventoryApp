using InventoryAppAPI.DAL;
using InventoryAppAPI.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InventoryAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuth(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.SetupDatabaseProvider(builder.Configuration);

            builder.Services.AddIdentity();

            builder.Services.AddServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            var app = builder.Build();

            app.Configure();

            app.SeedDatabase();

            app.Run();
        }
    }
}