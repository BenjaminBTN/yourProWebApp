using Microsoft.Extensions.DependencyInjection.Extensions;
using YourProfessionWebApp.Domain;
using YourProfessionWebApp.Domain.Repositories.Interfaces;
using YourProfessionWebApp.Domain.Repositories.TempImplementations;
using YourProfessionWebApp.Service;

namespace YourProfessionWebApp {
    public class Program {
        public static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			builder.Configuration.Bind("Project", new Config());

            builder.Services.AddScoped<IProfessionItemRepository, TempProfessionItemRepository>();
            builder.Services.AddScoped<ITextFieldRepository, TempTextFieldRepository>();
            builder.Services.AddScoped<IInterestRepository, TempInterestRepository>();

            // ?????
            builder.Services.AddDbContext<Context>();

			var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

			app.UseRouting();

			app.UseStaticFiles();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            // �� �� �����, ��� � ���� (?)
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            app.Run();
        }
    }
}