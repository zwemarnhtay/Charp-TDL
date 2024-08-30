using TDL.Domain.Enums;

namespace TDL.Application.Interfaces.Repositories;

public interface IRepository<T>
{
  Task<Result> CreateAsync(T entity);
  Task<T> GetByIdAsync(string id);
  Task<Result> UpdateAsync(T entity);
  Task<Result> DeleteAsync(string id);
}
