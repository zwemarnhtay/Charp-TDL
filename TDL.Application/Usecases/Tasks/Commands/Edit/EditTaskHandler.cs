using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Edit;

public class EditTaskHandler : IRequestHandler<EditTaskCommand, ResponseDto<TaskDto>>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public EditTaskHandler(IRepository<TaskEntity> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(EditTaskCommand request, CancellationToken cancellationToken)
  {
    var task = new TaskEntity
    {
      Id = request.Id,
      Title = request.Title,
      Description = request.Description,
      Deadline = request.Deadline,
      IsCompleted = request.IsCompleted,
      UserId = request.UserId,
    };

    var result = await _taskRepository.UpdateAsync(task, cancellationToken);

    if (result == Result.failed)
    {
      return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound, "fail to edit");
    }

    return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "edited success");
  }
}
