using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TDL.Application.Usecases.Tasks.Commands.Create;

namespace TDL.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddScoped<IValidator<CreateTaskCommand>, CreateTaskCommandValidator>();
    return services;
  }
}
