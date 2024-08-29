using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Create;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, ResponseDto<TaskDto>>
{
  private readonly ITaskRepository _taskRepository;

  public CreateTaskHandler(ITaskRepository taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
  {
    var task = new TaskEntity
    {
      Id = Guid.NewGuid().ToString(),
      Title = request.Title,
      Description = request.Description,
      Deadline = request.Deadline,
      IsCompleted = false,
      UserId = request.UserId,
    };

    var result = await _taskRepository.CreateAsync(task);

    if (result != Result.success)
    {
      return new ResponseDto<TaskDto>
      {
        StatusCode = ResponseStatusCode.BadRequest,
        IsSuccess = false,
        Message = "failed to create new task"
      };
    }

    return new ResponseDto<TaskDto>
    {
      StatusCode = ResponseStatusCode.OK,
      IsSuccess = true,
      Message = "created new task successful"
    };
  }
}
