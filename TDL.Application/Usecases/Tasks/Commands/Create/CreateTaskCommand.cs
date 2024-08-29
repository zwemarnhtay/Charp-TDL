using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Commands.Create;

public record CreateTaskCommand(string Title, string Description,
                  DateTime Deadline, string UserId) : IRequest<ResponseDto<TaskDto>>;

