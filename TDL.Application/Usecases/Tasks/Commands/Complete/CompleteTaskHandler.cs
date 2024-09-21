using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Complete;

public class CompleteTaskHandler : IRequestHandler<CompleteTaskCommand, ResponseDto<TaskDto>>
{
  private readonly ITaskRepository _taskRepository;

  public CompleteTaskHandler(ITaskRepository taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var result = await _taskRepository.CompleteAsync(request.id, cancellationToken);

      if (result == Result.failed) return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound, "no data to complete");

      return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "completed success");
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
