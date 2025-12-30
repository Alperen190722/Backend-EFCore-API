using D38_WebApiDemo.CustomMiddlewares;
using D38_WebApiDemo.DataAcess;
using D38_WebApiDemo.Formatters;

namespace D38_WebApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IProductDal,EfProductDal>();
            builder.Services.AddMvc(options => 
            { 
                options.OutputFormatters.Add(new VcardOutputFormatter()); 
            });
            builder.Services.AddAuthentication("Basic")
            .AddCookie("Basic", options => {// Yetkisiz giriþte yönlendirmeyi iptal edip direkt 401 dönmesini saðlarýz
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            }); // Sisteme "bak bir þemamýz var" diyoruz.

            builder.Services.AddAuthorization(); // <--- BU SATIRI EKLE (Eksik olan buydu)

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); 
            
            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthentication(); // 2. "Kim bu?" (Az önce eklediðimiz þemayý kullanýr)

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
