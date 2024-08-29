using MediatR;
using Microsoft.AspNetCore.Mvc;
using TDL.Application.DTOs;
using TDL.Application.Usecases.Tasks.Commands.Create;

namespace TDL.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
  private readonly IMediator _mediator;

  public TaskController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateTask(CreateTaskCommand request, CancellationToken cancelToken)
  {
    ResponseDto<TaskDto> result = await _mediator.Send(request, cancelToken);
    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(result), "application/json");
  }
}
