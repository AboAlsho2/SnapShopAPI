
using Microsoft.EntityFrameworkCore;
using SnapShop.Core.Repositories;
using SnapShop.Repository;
using SnapShop.Repository.Data;

namespace SnapShop.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try { 
            
            var dbContext = services.GetRequiredService<ShopContext>();
            await dbContext.Database.MigrateAsync();

            await   ShopContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex) {

              var logger =   loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Connection Error ");
            
            }




            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
