using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TDL.Application.Usecases.Tasks.Commands.Complete;
using TDL.Application.Usecases.Tasks.Commands.Create;
using TDL.Application.Usecases.Tasks.Commands.Delete;
using TDL.Application.Usecases.Tasks.Commands.Edit;
using TDL.Application.Usecases.Tasks.Queries.Detail;
using TDL.Application.Usecases.Tasks.Queries.List;

namespace TDL.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly IValidator<CreateTaskCommand> _createValidator;
  private readonly IValidator<EditTaskCommand> _editValidator;

  public TaskController(IMediator mediator,
    IValidator<CreateTaskCommand> createValidator,
    IValidator<EditTaskCommand> editValidator)
  {
    _mediator = mediator;
    _createValidator = createValidator;
    _editValidator = editValidator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateTask(CreateTaskCommand request, CancellationToken cancelToken)
  {

    var validationResult = await _createValidator.ValidateAsync(request, cancelToken);
    if (!validationResult.IsValid)
    {
      return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
    }
    try
    {
      var result = await _mediator.Send(request, cancelToken);
      return result.IsSuccess
          ? Ok(result)
          : BadRequest(new { message = result.Message });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new { message = "An unexpected error occurred while creating the task." });
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetTask(string id, CancellationToken cancelToken)
  {
    var result = await _mediator.Send(new GetTaskByIdQuery(id), cancelToken);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpGet("{userId}/list")]
  public async Task<IActionResult> GetTaskList(string userId, CancellationToken cancelToken)
  {
    var result = await _mediator.Send(new GetTaskListByUserIdQuery(userId), cancelToken);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> EditTask(string id, EditTaskCommand request, CancellationToken cancelToken)
  {
    request.Id = id;

    var validator = await _editValidator.ValidateAsync(request);

    if (!validator.IsValid)
    {
      return BadRequest(validator.Errors);
    }

    var result = await _mediator.Send(request, cancelToken);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTask(string id, CancellationToken cancelToken)
  {
    var command = new DeleteTaskCommand(id);
    var result = await _mediator.Send(command, cancelToken);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpPut("{id}/complete")]
  public async Task<IActionResult> CompleteTask(string id, CancellationToken cancelToken)
  {
    var command = new CompleteTaskCommand(id);
    var result = await _mediator.Send(command, cancelToken);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }
}
