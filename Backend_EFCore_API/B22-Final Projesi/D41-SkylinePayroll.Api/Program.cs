
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Business.Concrete;
using SkylinePayroll.Core.Utilities.IoC;
using SkylinePayroll.Core.Utilities.Middleware;
using SkylinePayroll.Core.Utilities.Security.JWT;
using SkylinePayroll.Data.Abstract;
using SkylinePayroll.Data.Concrete.EntityFramework;
using System.Text;

namespace D41_SkylinePayroll.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IEmployeeDal, EfEmployeeDal>();

            builder.Services.AddScoped<IDepartmentDal, EfDepartmentDal>();

            builder.Services.AddScoped<IRoleDal, EfRoleDal>();

            builder.Services.AddScoped<ITerminationDal, EfTerminationDal>();

            builder.Services.AddScoped<IPayrollDal, EfPayrollDal>();

            builder.Services.AddScoped<IUserDal, EfUserDal>();

            builder.Services.AddScoped<ITokenHelper, JwtHelper>();

            builder.Services.AddScoped<IEmployeeService, EmployeeManager>();

            builder.Services.AddScoped<IDepartmentService, DepartmentManager>();

            builder.Services.AddScoped<IRoleService, RoleManager>();

            builder.Services.AddScoped<ITerminationService, TerminationManager>();

            builder.Services.AddScoped<IPayrollService, PayrollManager>();

            builder.Services.AddScoped<IAuthService, AuthManager>();

            builder.Services.AddScoped<IUserService, UserManager>();

            builder.Services.AddSingleton<IMailService, MailManager>();

            builder.Services.AddScoped<INotificationService, NotificationManager>();

            builder.Services.AddDbContext<SkylineContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
                    ValidAudience = builder.Configuration["TokenOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            app.UseCors("AllowOrigin");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionMiddleware>();

            ServiceTool.Create(builder.Services);

            app.Run();
        }
    }
}
