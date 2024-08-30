using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;

namespace TDL.Application.Usecases.Tasks.Queries.Detail;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public GetTaskByIdHandler(IRepository<TaskEntity> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
  {
    var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

    if (task is null) return null;

    return new TaskDto(task.Id, task.Title, task.Description, task.Deadline, task.IsCompleted, task.UserId);
  }
}
