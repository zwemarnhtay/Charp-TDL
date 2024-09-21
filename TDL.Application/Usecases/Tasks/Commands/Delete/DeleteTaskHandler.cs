using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Delete;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, ResponseDto<TaskDto>>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public DeleteTaskHandler(IRepository<TaskEntity> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var result = await _taskRepository.DeleteAsync(request.id, cancellationToken);

      if (result == Result.failed) return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound, "fail to delete");

      return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "deleted success");
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
