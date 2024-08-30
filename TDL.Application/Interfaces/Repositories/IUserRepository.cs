using TDL.Domain.Entities;

namespace TDL.Application.Interfaces.Repositories;

public interface IUserRepository
{
  Task<List<UserEntity>> GetAllAsync();
  Task<UserEntity> GetByEmailAsync(string email);
}
