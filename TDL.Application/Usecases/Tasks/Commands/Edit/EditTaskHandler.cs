using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Application.Mappings;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Edit;

public class EditTaskHandler : IRequestHandler<EditTaskCommand, ResponseDto<TaskDto>>
{
  private readonly IRepository<TaskEntity> _taskRepository;

  public EditTaskHandler(IRepository<TaskEntity> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(EditTaskCommand request, CancellationToken cancellationToken)
  {
    var result = await _taskRepository.UpdateAsync(request.Map(), cancellationToken);

    if (result == Result.failed)
    {
      return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound, "fail to edit");
    }

    return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "edited success");
  }
}
