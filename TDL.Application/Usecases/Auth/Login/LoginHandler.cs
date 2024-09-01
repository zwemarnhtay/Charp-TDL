using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Helpers;
using TDL.Application.Interfaces.Repositories;
using TDL.Application.Mappings;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ResponseDto<String>>
{
  private readonly IUserRepository _userRepository;
  private readonly JwtGenerator _jwtGenerator;

  public LoginHandler(IUserRepository userRepository, JwtGenerator jwtGenerater)
  {
    _userRepository = userRepository;
    _jwtGenerator = jwtGenerater;
  }

  public async Task<ResponseDto<String>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var account = await _userRepository.GetByEmailAsync(request.email, cancellationToken);

      if (account == null || request.password != account.Password)
      {
        return ResponseDto<String>.Fail(ResponseStatusCode.NotFound, "incorrect email or password");
      }

      string jwtToken = _jwtGenerator.GenerateToken(account.Map());
      return ResponseDto<String>.Success(ResponseStatusCode.OK, "login success", jwtToken);
    }
    catch (TaskCanceledException ex)
    {
      return ResponseDto<String>.Fail(ResponseStatusCode.RequestCanceled, ex.ToString());
    }
    catch (Exception ex)
    {
      return ResponseDto<String>.Fail(ResponseStatusCode.InternalServerError, ex.ToString());
    }
  }
}
