using MongoDB.Bson.Serialization.Attributes;

namespace TDL.Domain.Entities;

public class UserEntity
{
  [BsonId]
  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }

}
