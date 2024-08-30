using TDL.Domain.Entities;

namespace TDL.Application.Interfaces.Repositories;

public interface ITaskRepository
{
  Task<List<TaskEntity>> GetAllByUserIdAsync(string userId);
}
