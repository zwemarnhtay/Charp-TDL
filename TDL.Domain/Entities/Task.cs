namespace TDL.Domain.Entities;

public class Task
{
  public string Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateOnly Deadline { get; set; }
  public bool IsCompleted { get; set; }
  public string UserId { get; set; }
}
