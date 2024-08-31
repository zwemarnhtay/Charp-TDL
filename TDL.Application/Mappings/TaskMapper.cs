using TDL.Application.DTOs;
using TDL.Application.Usecases.Tasks.Commands.Create;
using TDL.Application.Usecases.Tasks.Commands.Edit;
using TDL.Domain.Entities;

namespace TDL.Application.Mappings;

public static class TaskMapper
{
  public static TaskEntity Map(this CreateTaskCommand request)
  {
    return new TaskEntity
    {
      Id = Guid.NewGuid().ToString(),
      Title = request.Title,
      Description = request.Description,
      Deadline = request.Deadline,
      IsCompleted = false,
      UserId = request.UserId,
    };
  }

  public static TaskEntity Map(this EditTaskCommand request)
  {
    return new TaskEntity
    {
      Id = request.Id,
      Title = request.Title,
      Description = request.Description,
      Deadline = request.Deadline,
      IsCompleted = request.IsCompleted,
      UserId = request.UserId,
    };
  }

  public static TaskDto Map(this TaskEntity entity)
  {
    return new TaskDto
    (
      Id: entity.Id,
      Title: entity.Title,
      Description: entity.Description,
      Deadline: entity.Deadline,
      IsCompleted: entity.IsCompleted,
      UserId: entity.UserId
    );
  }
}
