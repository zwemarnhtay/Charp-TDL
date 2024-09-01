using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Infrastructure.Db;
using TDL.Infrastructure.Repositories;

namespace TDL.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    string connectionString = configuration.GetSection("MongoDBSettings:ConnectionString").Value!;
    string databaseName = configuration.GetSection("MongoDBSettings:DatabaseName").Value!;

    string key = configuration.GetSection("JWT:Key").Value!;
    string issuer = configuration.GetSection("JWT:Issuer").Value!;
    string audience = configuration.GetSection("JWT:Audience").Value!;

    services.AddScoped(opt =>
      new MongoDbConnection(connectionString, databaseName)
      );

    services.AddAuthentication(opt =>
    {
      opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(opt =>
    {
      var KEY = Encoding.UTF8.GetBytes(key);
      opt.SaveToken = true;
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(KEY)
      };
    });

    services.AddScoped<IRepository<TaskEntity>, TaskRepository>();
    services.AddScoped<IRepository<UserEntity>, UserRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ITaskRepository, TaskRepository>();

    return services;
  }
}
