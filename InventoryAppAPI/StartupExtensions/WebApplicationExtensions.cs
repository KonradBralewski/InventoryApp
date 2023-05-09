using InventoryAppAPI.DAL;

namespace InventoryAppAPI.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void SeedDatabase(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            seeder.Seed();

        }
        public static void Configure(this WebApplication app) 
        {
            app.UseExceptionHandler("/error");

            if (app.Environment.IsDevelopment())
            {
                app.UseCors(ServiceCollectionExtensions.policyName);
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
