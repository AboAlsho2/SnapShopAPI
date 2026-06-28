
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SnapShop.APIs.Errors;
using SnapShop.APIs.Helpers;
using SnapShop.APIs.Middlewares;
using SnapShop.Core.Models.Identity;
using SnapShop.Core.Repositories;
using SnapShop.Repository;
using SnapShop.Repository.Data;
using SnapShop.Repository.Identity;
using StackExchange.Redis;

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

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            builder.Services.AddAuthentication();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>();


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count() > 0)
                                                        .SelectMany(e => e.Value.Errors)
                                                        .Select(e => e.ErrorMessage)
                                                        .ToList();
                    var validationResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationResponse);
                };
            });


            // builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfiles()));

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {

                var dbContext = services.GetRequiredService<ShopContext>();
                await dbContext.Database.MigrateAsync();
                var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();
                await ShopContextSeed.SeedAsync(dbContext);
                var manger = services.GetRequiredService<UserManager<AppUser>>();
                await  AppIdentityDbContextSeed.SeedUserAsync(manger);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Connection Error ");

            }

            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
