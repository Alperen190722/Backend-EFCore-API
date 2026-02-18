
using CloudinaryDotNet;
using D39_SehirRehberi.API.Data;
using D39_SehirRehberi.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace D39_SehirRehberi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            builder.Services.AddDbContext<DataContext>(x => 
            x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
            builder.Services.AddCors();
            // CloudinarySettings'i okuyup Account nesnesini oluþturuyoruz
            var cloudinaryConfig = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();

            Account account = new Account(
                cloudinaryConfig.CloudName,
                cloudinaryConfig.ApiKey,
                cloudinaryConfig.ApiSecret
            );

            // Cloudinary nesnesini sisteme Singleton olarak ekliyoruz
            builder.Services.AddSingleton(new Cloudinary(account));

            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters 
                { 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddControllers().AddJsonOptions(opt => {
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors(x=>x.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
