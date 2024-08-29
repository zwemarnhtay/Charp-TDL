using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;

namespace TDL.Application.Usecases.Tasks.Queries.List;

public class GetTaskListByUserIdHandler : IRequestHandler<GetTaskListByUserIdQuery, List<TaskDto>>
{
  private readonly ITaskRepository _taskRepository;

  public GetTaskListByUserIdHandler(ITaskRepository taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<List<TaskDto>?> Handle(GetTaskListByUserIdQuery request, CancellationToken cancellationToken)
  {
    var list = await _taskRepository.GetAllByUserIdAsync(request.UserId);

    if (list == null) return null;

    var dtoList = list.Select(l =>
            new TaskDto(l.Id, l.Title, l.Description, l.Deadline, l.IsCompleted, l.UserId)
            ).ToList();

    return dtoList;
  }
}
