using TDL.Domain.Enums;

namespace TDL.Application.Interfaces.Repositories;

public interface IRepository<T>
{
  Task<Result> CreateAsync(T entity, CancellationToken cancelToken);
  Task<T> GetByIdAsync(string id, CancellationToken cancelToken);
  Task<Result> UpdateAsync(T entity, CancellationToken cancelToken);
  Task<Result> DeleteAsync(string id, CancellationToken cancelToken);
}
