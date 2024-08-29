using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Commands.Create;

public record CreateTaskCommand(string Title, string Description,
                  DateOnly Deadline, string UserId) : IRequest<ResponseDto<TaskDto>>;

