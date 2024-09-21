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

  public async Task<Result> CreateAsync(TaskEntity entity, CancellationToken cancelToken)
  {
    await _tasks.InsertOneAsync(entity, cancellationToken: cancelToken);
    return Result.success;
  }

  public async Task<Result> DeleteAsync(string id, CancellationToken cancelToken)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, id);
    var result = await _tasks.DeleteOneAsync(filter, cancelToken);

    if (result.DeletedCount > 0)
    {
      return Result.success;
    }
    return Result.failed;
  }

  public async Task<List<TaskEntity>> GetAllByUserIdAsync(string userId, CancellationToken cancelToken)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId);

    var tasks = await _tasks.Find(filter).ToListAsync(cancelToken);
    return tasks;
  }

  public async Task<TaskEntity> GetByIdAsync(string id, CancellationToken cancelToken)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, id);
    var task = await _tasks.Find(filter).FirstOrDefaultAsync(cancelToken);
    return task;
  }

  public async Task<Result> UpdateAsync(TaskEntity entity, CancellationToken cancelToken)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, entity.Id);
    var result = await _tasks.ReplaceOneAsync(filter, entity, cancellationToken: cancelToken);

    if (result.ModifiedCount > 0)
    {
      return Result.success;
    }
    return Result.failed;
  }

  public async Task<Result> CompleteAsync(string id, CancellationToken cancleToken)
  {
    var filter = Builders<TaskEntity>.Filter.Eq(t => t.Id, id);

    var update = Builders<TaskEntity>.Update.Set(t => t.IsCompleted, true);

    var result = await _tasks.UpdateOneAsync(filter, update, cancellationToken: cancleToken);

    if (result.ModifiedCount > 0)
    {
      return Result.success;
    }
    return Result.failed;
  }
}
