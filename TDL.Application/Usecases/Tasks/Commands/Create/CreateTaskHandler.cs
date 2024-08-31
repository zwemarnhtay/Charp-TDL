using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Create;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, ResponseDto<TaskDto>>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public CreateTaskHandler(IRepository<TaskEntity> taskRepository)
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

    var result = await _taskRepository.CreateAsync(task, cancellationToken);

    if (result != Result.success)
    {
      return ResponseDto<TaskDto>.Fail(ResponseStatusCode.BadRequest, "fali to create new task");
    }

    return ResponseDto<TaskDto>.Success(ResponseStatusCode.Created, "created success");
  }
}
