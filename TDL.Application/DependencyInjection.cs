using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TDL.Application.Helpers;
using TDL.Application.Usecases.Auth.Login;
using TDL.Application.Usecases.Auth.Register;
using TDL.Application.Usecases.Tasks.Commands.Create;
using TDL.Application.Usecases.Tasks.Commands.Edit;

namespace TDL.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddScoped<JwtGenerator>();

    services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();
    services.AddScoped<IValidator<LoginCommand>, LoginValidator>();

    services.AddScoped<IValidator<CreateTaskCommand>, CreateTaskValidator>();
    services.AddScoped<IValidator<EditTaskCommand>, EditTaskValidator>();
    return services;
  }
}
