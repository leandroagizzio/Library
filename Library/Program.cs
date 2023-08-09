using CoreLibrary.Data;
using CoreLibrary.Operations;
using CoreLibrary.Operations.Interfaces;
using CoreLibrary.Repositories;
using CoreLibrary.Repositories.Interfaces;
using Library.Helper;
using Library.Helper.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Context>(o => 
                o.UseSqlite(builder.Configuration.GetConnectionString("DataBaseSqLite"), b => b.MigrationsAssembly("Library"))
            );
            
            //userSession
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookProcess, BookProcess>();
            builder.Services.AddScoped<IQueueProcess, QueueProcess>();

            builder.Services.AddScoped<IUserSession, UserSession>();

            //userSession
            builder.Services.AddSession(o => {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //userSession
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}