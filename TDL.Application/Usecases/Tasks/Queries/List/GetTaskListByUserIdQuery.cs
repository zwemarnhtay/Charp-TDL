using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Tasks.Queries.List;

public record GetTaskListByUserIdQuery(string UserId) : IRequest<ResponseDto<List<TaskDto>>>;
