using BackendChallenge.Core.Extensions;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Core.Interfaces.Seeding;
using BackendChallenge.Core.Interfaces.Services;
using BackendChallenge.Core.Interfaces.Users;
using BackendChallenge.Infrastructure.Context;
using BackendChallenge.Infrastructure.Repositories;
using BackendChallenge.Infrastructure.Seedings;
using BackendChallenge.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using User = BackendChallenge.Core.Interfaces.Users.User;

namespace BackendChallenge.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddServiceInterfaces(this IServiceCollection services)
        {
            //scoped
            services.AddScoped<IUser, User>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEntryService, EntryService>();

            //transient
            services.AddTransient<IDataSeeding, DataSeeding>();
        }

        public static void AddRepositoryInterfaces(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IFreeDictionaryRepository, FreeDictionaryRepository>();
        }

        public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IFreeDictionaryRepository, FreeDictionaryRepository>(client =>
            {
                client.BaseAddress = new Uri(configuration["FreeDictionaryAPI"]);
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BcContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        }

        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            AesExtension.Configure(configuration["AES:Key"], configuration["AES:IV"]);
            var key = Encoding.ASCII.GetBytes(configuration["SecretKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend Challenge", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization Header - Bearer Authentication.\r\n\r\n" +
                                  "Digite 'Bearer' [espaço] e o token no campo abaixo.\r\n\r\n" +
                                  "Exemplo: Bearer a1b2c3d4",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
