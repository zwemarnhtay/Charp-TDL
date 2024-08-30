using MongoDB.Driver;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;
using TDL.Infrastructure.Db;

namespace TDL.Infrastructure.Repositories;

public class TaskRepository : IRepository<TaskEntity>, ITaskRepository
{
  private readonly IMongoCollection<TaskEntity> _tasks;

  public TaskRepository(MongoDbConnection mongodb)
  {
    _tasks = mongodb.GetCollection<TaskEntity>("tasks");
  }

  public async Task<Result> CreateAsync(TaskEntity entity)
  {
    await _tasks.InsertOneAsync(entity);
    return Result.success;
  }

  public async Task<Result> DeleteAsync(string id)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, id);
    var result = await _tasks.DeleteOneAsync(filter);

    if (result.DeletedCount > 0)
    {
      return Result.success;
    }
    return Result.failed;
  }

  public async Task<List<TaskEntity>> GetAllByUserIdAsync(string userId)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId);

    var tasks = await _tasks.Find(filter).ToListAsync();
    return tasks;
  }

  public async Task<TaskEntity> GetByIdAsync(string id)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, id);
    var task = await _tasks.Find(filter).FirstOrDefaultAsync();
    return task;
  }

  public async Task<Result> UpdateAsync(TaskEntity entity)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, entity.Id);
    var result = await _tasks.ReplaceOneAsync(filter, entity);

    if (result.ModifiedCount > 0)
    {
      return Result.success;
    }
    return Result.failed;
  }

}
