using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Tasks.Commands.Delete;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, ResponseDto>
{
  private readonly IRepository<TaskEntity> _TaskRepository;

  public DeleteTaskHandler(IRepository<TaskEntity> taskRepository)
  {
    _TaskRepository = taskRepository;
  }

  public async Task<ResponseDto> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
  {
    var result = await _TaskRepository.DeleteAsync(request.id, cancellationToken);

    if (result == Result.failed)
    {
      return new ResponseDto
      {
        StatusCode = ResponseStatusCode.NotFound,
        IsSuccess = false,
        Message = "failed to delete"
      };
    }

    return new ResponseDto
    {
      StatusCode = ResponseStatusCode.OK,
      IsSuccess = false,
      Message = "deleted success"
    };
  }
}
