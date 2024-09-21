using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Interfaces.Repositories;

public interface ITaskRepository
{
  Task<List<TaskEntity>> GetAllByUserIdAsync(string userId, CancellationToken cancelToken);

  Task<Result> CompleteAsync(string id, CancellationToken cancelToken);
}
