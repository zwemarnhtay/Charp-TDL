using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Commands.Delete;

public record DeleteTaskCommand(string id) : IRequest<ResponseDto>;
