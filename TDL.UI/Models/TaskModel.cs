namespace TDL.UI.Models;

public class TaskModel
{
  public string Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime Deadline { get; set; }
  public bool IsCompleted { get; set; }
  public string UserId { get; set; }

}
