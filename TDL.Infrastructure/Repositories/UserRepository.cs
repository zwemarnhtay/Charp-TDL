using MongoDB.Driver;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;
using TDL.Infrastructure.Db;

namespace TDL.Infrastructure.Repositories
{
  public class UserRepository : IRepository<UserEntity>, IUserRepository
  {
    private IMongoCollection<UserEntity> _users;

    public UserRepository(MongoDbConnection mongodb)
    {
      _users = mongodb.GetCollection<UserEntity>("users");
    }

    public async Task<Result> CreateAsync(UserEntity entity, CancellationToken cancelToken)
    {
      await _users.InsertOneAsync(entity, cancellationToken: cancelToken);
      return Result.success;
    }

    public async Task<Result> DeleteAsync(string id, CancellationToken cancelToken)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, id);
      var result = await _users.DeleteOneAsync(filter, cancelToken);

      if (result.DeletedCount > 0)
      {
        return Result.success;
      }
      return Result.failed;
    }

    public async Task<List<UserEntity>> GetAllAsync(CancellationToken cancelToken)
    {
      var users = await _users.Find(task => true).ToListAsync(cancelToken);
      return users;
    }

    public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancelToken)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Email, email);
      var user = await _users.Find(filter).FirstOrDefaultAsync(cancelToken);
      return user;
    }

    public async Task<UserEntity> GetByIdAsync(string id, CancellationToken cancelToken)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, id);
      var user = await _users.Find(filter).FirstOrDefaultAsync(cancelToken);
      return user;
    }

    public async Task<Result> UpdateAsync(UserEntity entity, CancellationToken cancelToken)
    {
      var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, entity.Id);
      var result = await _users.ReplaceOneAsync(filter, entity, cancellationToken: cancelToken);

      if (result.ModifiedCount > 0)
      {
        return Result.success;
      }
      return Result.failed;
    }
  }
}
