using TDL.Application.Interfaces;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Infrastructure.Repositories
{
  public class UserRepository : IRepositories<User>
  {
    public Task<Result> Create(User entity)
    {
      throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<List<User>> GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<User> GetAsync(User entity)
    {
      throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(string id)
    {
      throw new NotImplementedException();
    }
  }
}
