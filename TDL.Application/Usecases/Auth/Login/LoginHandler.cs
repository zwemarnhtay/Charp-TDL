using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Helpers;
using TDL.Application.Interfaces.Repositories;
using TDL.Application.Mappings;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ResponseDto<UserDto>>
{
  private readonly IUserRepository _userRepository;
  private readonly JwtGenerator _jwtGenerator;

  public LoginHandler(IUserRepository userRepository, JwtGenerator jwtGenerator)
  {
    _userRepository = userRepository;
    _jwtGenerator = jwtGenerator;
  }

  public async Task<ResponseDto<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var account = await _userRepository.GetByEmailAsync(request.email, cancellationToken);

      if (account == null || request.password != account.Password)
      {
        return ResponseDto<UserDto>.Fail(ResponseStatusCode.NotFound, "incorrect email or password");
      }

      string jwtToken = _jwtGenerator.GenerateToken(account.Map());
      return ResponseDto<UserDto>.SuccessLogin(ResponseStatusCode.OK, jwtToken, account.Map());
    }
    catch (TaskCanceledException ex)
    {
      return ResponseDto<UserDto>.Fail(ResponseStatusCode.RequestCanceled, ex.ToString());
    }
    catch (Exception ex)
    {
      return ResponseDto<UserDto>.Fail(ResponseStatusCode.InternalServerError, ex.ToString());
    }
  }
}
