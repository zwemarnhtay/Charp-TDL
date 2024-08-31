using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Commands.Edit;

public class EditTaskCommand : IRequest<ResponseDto<TaskDto>>
{
  public string Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime Deadline { get; set; }
  public bool IsCompleted { get; set; }
  public string UserId { get; set; }
}
