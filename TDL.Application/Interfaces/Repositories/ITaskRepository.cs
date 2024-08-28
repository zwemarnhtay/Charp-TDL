using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Interfaces.Repositories;

public interface ITaskRepository
{
  Task<Result> CreateAsync(TaskEntity entity);
  Task<List<TaskEntity>> GetAllByUserIdAsync(string userId);
  Task<TaskEntity> GetByIdAsync(string id);
  Task<Result> UpdateAsync(TaskEntity entity);
  Task<Result> DeleteAsync(string id);
}
