using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TDL.Application.Usecases.Auth.Login;
using TDL.Application.Usecases.Auth.Register;

namespace TDL.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly IValidator<RegisterCommand> _registerValidator;
  private readonly IValidator<LoginCommand> _loginValidator;

  public AuthController(IMediator mediator,
    IValidator<RegisterCommand> registerValidator,
    IValidator<LoginCommand> loginValidator)
  {
    _mediator = mediator;
    _registerValidator = registerValidator;
    _loginValidator = loginValidator;
  }


  [HttpPost("Register")]
  public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancelToken)
  {
    var validator = await _registerValidator.ValidateAsync(request, cancelToken);

    if (!validator.IsValid) return BadRequest(validator.Errors);


    var result = await _mediator.Send(request, cancelToken);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpPost("Login")]
  public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancelToken)
  {
    var validator = await _loginValidator.ValidateAsync(request, cancelToken);

    if (!validator.IsValid) return BadRequest(validator.Errors);

    var result = await _mediator.Send(request, cancelToken);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }
}
