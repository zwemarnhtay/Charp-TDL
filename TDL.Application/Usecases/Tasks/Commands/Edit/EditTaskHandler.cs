using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Edit;

public class EditTaskHandler : IRequestHandler<EditTaskCommand, ResponseDto>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public EditTaskHandler(IRepository<TaskEntity> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto> Handle(EditTaskCommand request, CancellationToken cancellationToken)
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

    var result = await _taskRepository.UpdateAsync(task);

    if (result == Result.failed)
    {
      return new ResponseDto
      {
        StatusCode = ResponseStatusCode.NotFound,
        IsSuccess = false,
        Message = "You can't edit this task"
      };
    }

    return new ResponseDto
    {
      StatusCode = ResponseStatusCode.OK,
      IsSuccess = true,
      Message = "edited task success"
    };
  }
}
