using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StormBackend.Data;
using StormBackend.Models;
using StormBackend.Repository;
using StormBackend.Repository.Contacts;
using StormBackend.Services;
using StormBackend.Services.Contacts;

namespace StormBackend.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            DotNetEnv.Env.Load();
            var connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING");
            
            services.AddDbContext<AppDBContext>(
                options => options.UseSqlServer(
                    connectionString, 
                    a => a.CommandTimeout(180)));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                }
            ).AddEntityFrameworkStores<AppDBContext>();

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = 
                    options.DefaultChallengeScheme =
                    options.DefaultForbidScheme =
                    options.DefaultScheme =
                    options.DefaultSignInScheme =
                    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
                    }

        public static void ConfigureSwaggerGen(this IServiceCollection services){
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Roommate", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    
        public static void ConfigureControllers(this IServiceCollection services){
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) => 
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureServices(this IServiceCollection services){
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserManager>();
        }
        public static void ConfigureRepositories(this IServiceCollection services){
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}