using MongoDB.Bson.Serialization.Attributes;

namespace TDL.Domain.Entities;

public class TaskEntity
{
  [BsonId]
  public string Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime Deadline { get; set; }
  public bool IsCompleted { get; set; }
  public string UserId { get; set; }
}
