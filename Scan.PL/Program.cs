
using AutoMapper;
using Domain.Contracts;
using IKEa.DAL.Persinstance.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance.Data;
using Scan.BLL.Mappings;
using Scan.BLL.Services;
using Scan.BLL.Services.Ai;
using Scan.BLL.Services.AiServices;
using Scan.BLL.Services.AnalysisServices;
using Scan.BLL.Services.Attachments;
using Scan.BLL.Services.AuthenticationServices;
using Scan.BLL.Services.CarServices;
using Scan.BLL.Services.DamageServices;
using Scan.BLL.Services.DetectionServices;
using Scan.BLL.Services.EmailServices;
using Scan.BLL.Services.RepairCenterServices;
using Scan.BLL.Services.User_Services;
using Scan.DAL.Persistance.Repositories.CarRepositories;
using Scan.DAL.Persistance.Repositories.DamageDetailRepositories;
using Scan.DAL.Persistance.Repositories.DetectionRepairCenters;
using Scan.DAL.Persistance.Repositories.Detections;
using Scan.DAL.Persistance.Repositories.RepairCenters;
using Scan.DAL.Persistance.Repositories.Users;
using Scan.DAL.Persistance.UnitOfWork;
using Scan.PL.Middleware;
using System.Text;
using System.Text.Json;

namespace Scan.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {



                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));


            });
            builder.Services.AddScoped<IDbInializer, DbInializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddAutoMapper(typeof(CarScanAiMappingProfile).Assembly);

            builder.Services.AddScoped<IAuthenticationServices, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICarService, CarService>();

            builder.Services.AddScoped<IDetectionService, DetectionService>();
            builder.Services.AddScoped<IDamageService, DamageService>();
            builder.Services.AddScoped<IRepairCenterService, RepairCenterService>();
            builder.Services.AddScoped<IAnalysisService, AnalysisService>();
            builder.Services.AddScoped<IEmailService, SmtpEmailService>();
            builder.Services.AddScoped<IUrlService, UrlService>();
            //builder.Services.AddScoped<IFakeCarDamageAiService, FakeCarDamageAiService>();
            builder.Services.AddHttpClient<
    ICarDamageAiService,
    FakeCarDamageAiService>();


            builder.Services.AddHttpContextAccessor();



            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Token like this: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                 {
                     new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                         },
                             new string[] {}
                         }
                     });
            });



            builder.Services
            .AddIdentity<User, IdentityRole>((Option) =>
            {
                Option.User.RequireUniqueEmail = true;
                //Option.Password.RequireDigit = true;
                //Option.Password.RequiredLength = 8;
                Option.Password.RequiredUniqueChars = 0;
                Option.Password.RequireNonAlphanumeric = false;
                Option.Password.RequireUppercase = false;
                Option.Password.RequireLowercase = false;

                Option.Lockout.AllowedForNewUsers = true;
                Option.Lockout.MaxFailedAccessAttempts = 20;


            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();




            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                config.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters()
            {
                ValidateIssuer = true,

                ValidIssuer =
                    builder.Configuration["JwtOptions:Issuer"],

                ValidateAudience = true,

                ValidAudience =
                    builder.Configuration["JwtOptions:Audience"],

                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration[
                                "JwtOptions:SecretKey"]))
            };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = 401;

                context.Response.ContentType =
                    "application/json";

                var response =
                    JsonSerializer.Serialize(new
                    {
                        StatusCode = 401,

                        ErrorMessage =
                            "Unauthorized. Invalid or missing token."
                    });

                await context.Response.WriteAsync(
                    response);
            },

            OnForbidden = async context =>
            {
                context.Response.StatusCode = 403;

                context.Response.ContentType =
                    "application/json";

                var response =
                    JsonSerializer.Serialize(new
                    {
                        StatusCode = 403,

                        ErrorMessage =
                            "You do not have permission to access this resource."
                    });

                await context.Response.WriteAsync(
                    response);
            }
        };
    });


            var app = builder.Build();

            app.UseCors("AllowAll");

            await InitializeDbAsync(app);

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await context.Database.MigrateAsync();
                }
            }

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.UseStaticFiles();



            app.Run();
        }
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope(); //using to automatic deleted by clr dispose created scope by di

            var dbInializer = scope.ServiceProvider.GetRequiredService<IDbInializer>();
            await dbInializer.InializeAsync();
            await dbInializer.IdentityInializeAsync();
        }
    }
}
