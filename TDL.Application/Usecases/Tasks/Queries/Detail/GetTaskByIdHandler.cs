using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Application.Mappings;
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
    try
    {
      var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

      if (task is null) return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound, "data not found");

      return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "data found", task.Map());
    }
    catch (TaskCanceledException ex)
    {
      return ResponseDto<TaskDto>.Fail(ResponseStatusCode.RequestCanceled, ex.ToString());
    }
    catch (Exception ex)
    {
      return ResponseDto<TaskDto>.Fail(ResponseStatusCode.BadRequest, ex.ToString());
    }
  }
}
