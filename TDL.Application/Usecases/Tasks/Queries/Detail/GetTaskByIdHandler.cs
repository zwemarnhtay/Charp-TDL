using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Queries.Detail;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, ResponseDto<TaskDto>>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public GetTaskByIdHandler(IRepository<TaskEntity> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
  {
    var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

    if (task is null) return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound, "data not found");

    var data = new TaskDto(task.Id, task.Title, task.Description, task.Deadline, task.IsCompleted, task.UserId);

    return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "data found", data);
  }
}
