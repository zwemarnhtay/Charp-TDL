using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Interfaces.Repositories;

public interface IUserRepository
{
  Task<Result> CreateAsync(UserEntity entity);
  Task<List<UserEntity>> GetAllAsync();
  Task<UserEntity> GetByIdAsync(string id);
  Task<Result> UpdateAsync(UserEntity entity);
  Task<Result> DeleteAsync(string id);
}
