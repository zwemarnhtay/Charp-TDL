using System.ComponentModel.DataAnnotations;

namespace TDL.UI.Models;

public class TaskCreateModel
{
  [Required(ErrorMessage = "Title is requored")]
  public string Title { get; set; }
  public string Description { get; set; }
  [Required(ErrorMessage = "Deadline is required")]
  public DateTime Deadline { get; set; }
  public string UserId { get; set; }
}
