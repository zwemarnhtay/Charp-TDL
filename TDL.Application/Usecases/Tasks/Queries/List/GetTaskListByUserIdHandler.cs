using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Queries.List;

public class GetTaskListByUserIdHandler : IRequestHandler<GetTaskListByUserIdQuery, ResponseDto<List<TaskDto>>>
{
  private readonly ITaskRepository _taskRepository;

  public GetTaskListByUserIdHandler(ITaskRepository taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<List<TaskDto>>> Handle(GetTaskListByUserIdQuery request, CancellationToken cancellationToken)
  {
    var list = await _taskRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);

    if (list == null) return ResponseDto<List<TaskDto>>.Fail(ResponseStatusCode.NotFound, "data not found");

    var dtoList = list.Select(l =>
            new TaskDto(l.Id, l.Title, l.Description, l.Deadline, l.IsCompleted, l.UserId)
    ).ToList();

    return ResponseDto<List<TaskDto>>.Success(ResponseStatusCode.OK, "data found", dtoList);
  }
}
