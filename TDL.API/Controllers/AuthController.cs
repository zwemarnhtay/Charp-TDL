using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TDL.Application.Usecases.Auth.Register;

namespace TDL.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly IValidator<RegisterCommand> _registerValidator;

  public AuthController(IMediator mediator, IValidator<RegisterCommand> registerValidator)
  {
    _mediator = mediator;
    _registerValidator = registerValidator;
  }


  [HttpPost]
  public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancelToken)
  {
    var validator = await _registerValidator.ValidateAsync(request, cancelToken);

    if (!validator.IsValid) return BadRequest(validator.Errors);


    var result = await _mediator.Send(request, cancelToken);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }
}
