using MongoDB.Driver;

namespace TDL.Infrastructure.Db;

public class MongoDbConnection
{
  private readonly IMongoDatabase _mongodb;

  public MongoDbConnection(string connectionString, string databaseName)
  {
    var client = new MongoClient(connectionString);
    _mongodb = client.GetDatabase(databaseName);
  }

  public IMongoCollection<T> GetCollection<T>(string collectionName)
  {
    return _mongodb.GetCollection<T>(collectionName);
  }
}
