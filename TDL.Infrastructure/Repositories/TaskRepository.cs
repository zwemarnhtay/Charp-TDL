using TDL.Application.Interfaces;
using TDL.Domain.Enums;

namespace TDL.Infrastructure.Repositories
{
  public class TaskRepository : IRepositories<Task>
  {
    public Task<Result> Create(Task entity)
    {
      throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<List<Task>> GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Task> GetAsync(Task entity)
    {
      throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(string id)
    {
      throw new NotImplementedException();
    }
  }
}
