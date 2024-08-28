using MongoDB.Driver;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;
using TDL.Infrastructure.Db;

namespace TDL.Infrastructure.Repositories
{
  public class UserRepository : IUserRepository
  {
    private IMongoCollection<UserEntity> _users;

    public UserRepository(MongoDbConnection mongodb)
    {
      _users = mongodb.GetCollection<UserEntity>("users");
    }

    public async Task<Result> CreateAsync(UserEntity entity)
    {
      await _users.InsertOneAsync(entity);
      return Result.success;
    }

    public async Task<Result> DeleteAsync(string id)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, id);
      var result = await _users.DeleteOneAsync(filter);

      if (result.DeletedCount > 0)
      {
        return Result.success;
      }
      return Result.failed;
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
      var users = await _users.Find(task => true).ToListAsync();
      return users;
    }

    public async Task<UserEntity> GetByIdAsync(string id)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, id);
      var task = await _users.Find(filter).FirstOrDefaultAsync();
      return task;
    }

    public async Task<Result> UpdateAsync(UserEntity entity)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, entity.Id);
      var result = await _users.ReplaceOneAsync(filter, entity);

      if (result.ModifiedCount > 0)
      {
        return Result.success;
      }
      return Result.failed;
    }
  }
}
