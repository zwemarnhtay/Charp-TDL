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
  private readonly IRepository<UserEntity> _userRepository;

  public EditTaskHandler(IRepository<TaskEntity> taskRepository, IRepository<UserEntity> userRepository)
  {
    _taskRepository = taskRepository;
    _userRepository = userRepository;
  }

  public async Task<ResponseDto<TaskDto>> Handle(EditTaskCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var existedUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

      if (existedUser == null) return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound,
        "user id not found");

      var result = await _taskRepository.UpdateAsync(request.Map(), cancellationToken);

      if (result == Result.failed) return ResponseDto<TaskDto>.Fail(ResponseStatusCode.NotFound,
        "fail to edit");

      return ResponseDto<TaskDto>.Success(ResponseStatusCode.OK, "edited success");
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
