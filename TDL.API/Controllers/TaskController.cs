using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TDL.Application.Usecases.Tasks.Commands.Create;
using TDL.Application.Usecases.Tasks.Queries.Detail;
using TDL.Application.Usecases.Tasks.Queries.List;

namespace TDL.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly IValidator<CreateTaskCommand> _createValidator;

  public TaskController(IMediator mediator, IValidator<CreateTaskCommand> createValidator)
  {
    _mediator = mediator;
    _createValidator = createValidator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateTask(CreateTaskCommand request, CancellationToken cancelToken)
  {
    var validator = await _createValidator.ValidateAsync(request, cancelToken);

    if (!validator.IsValid)
    {
      return BadRequest(validator.Errors);
    }
    var result = await _mediator.Send(request, cancelToken);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpGet("id")]
  public async Task<IActionResult> GetTask(string id, CancellationToken cancelToken)
  {
    var result = await _mediator.Send(new GetTaskByIdQuery(id), cancelToken);

    return result is not null ? Ok(result) : NotFound();
  }

  [HttpGet("userId")]
  public async Task<IActionResult> GetTaskList(string userId, CancellationToken cancelToken)
  {
    var result = await _mediator.Send(new GetTaskListByUserIdQuery(userId), cancelToken);

    return result is not null ? Ok(result) : NotFound();
  }
}
