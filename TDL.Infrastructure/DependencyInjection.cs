using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TDL.Application.Interfaces.Repositories;
using TDL.Infrastructure.Db;
using TDL.Infrastructure.Repositories;

namespace TDL.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    string connectionString = configuration.GetSection("MongoDBSettings:ConnectionString").Value!;
    string databaseName = configuration.GetSection("MongoDBSettings:DatabaseName").Value!;

    services.AddScoped(opt =>
      new MongoDbConnection(connectionString, databaseName)
      );

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ITaskRepository, TaskRepository>();

    return services;
  }
}
