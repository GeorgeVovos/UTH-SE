using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uth.Recipes.DataAccess;
using Uth.Recipes.IOC;

namespace Uth.Recipes.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var iocContainer = DependencyInjectionManager.ConfigureProvider(builder.Services);
            
            var dbContextProvider = iocContainer.GetRequiredService<IRecipesDbContextProvider>();
            var context = dbContextProvider.GetDbContext();
            await context.Database.EnsureCreatedAsync();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
      
            var app = builder.Build();
            app.MapOpenApi();
          
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.MapControllers();

            app.Run();
        }
    }
}
