using TDL.Application.DTOs;
using TDL.Application.Usecases.Auth.Register;
using TDL.Domain.Entities;

namespace TDL.Application.Mappings;

public static class UserMapper
{
  public static UserEntity Map(this RegisterCommand request)
  {
    return new UserEntity
    {
      Id = Guid.NewGuid().ToString(),
      Name = request.Name,
      Email = request.Email,
      Password = request.Password,
    };
  }

  public static UserDto Map(this UserEntity entity)
  {
    return new UserDto
    (
      Id: entity.Id,
      Name: entity.Name,
      Email: entity.Email,
      Password: entity.Password
    );
  }
}
