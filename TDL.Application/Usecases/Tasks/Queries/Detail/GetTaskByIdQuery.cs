using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Queries.Detail;

public record GetTaskByIdQuery(string Id) : IRequest<TaskDto>;
