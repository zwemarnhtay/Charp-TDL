using TDL.Domain.Enums;

namespace TDL.Application.Interfaces;

public interface IRepositories<T>
{
  Task<Result> Create(T entity);
  Task<T> GetAsync(T entity);
  Task<Result> UpdateAsync(string id);
  Task<Result> DeleteAsync(string id);
  Task<List<T>> GetAllAsync();
}
