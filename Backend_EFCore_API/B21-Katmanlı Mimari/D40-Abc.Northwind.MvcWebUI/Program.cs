using Abc.Northwind.Bussines.Abstract;
using Abc.Northwind.Bussines.Concrete;
using Abc.Northwind.DataAccess.Abstract;
using Abc.Northwind.DataAccess.Concrete.EntityFramework;
using D40_Abc.Northwind.MvcWebUI.Entities;
using D40_Abc.Northwind.MvcWebUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace D40_Abc.Northwind.MvcWebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc(options=>options.EnableEndpointRouting=false);
            builder.Services.AddScoped<IProductService, ProductManager>();
            builder.Services.AddScoped<IProductDal, EfProductDal>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
            builder.Services.AddSingleton<ICartSessionService, CartSessionService>();
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDbContext<CustomIdentityDbContext>
                (options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NorthwindDB;Trusted_Connection=true"));
            builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddSession();
            builder.Services.AddDistributedMemoryCache();
            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); 


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });


            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);
            app.Run();
        }

        private static void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Product}/{action=Index}/{id?}");
        }
    }
}
