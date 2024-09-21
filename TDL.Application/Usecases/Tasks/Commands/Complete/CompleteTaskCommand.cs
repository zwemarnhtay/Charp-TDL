using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Commands.Complete;

public record CompleteTaskCommand(string id) : IRequest<ResponseDto<TaskDto>>;
