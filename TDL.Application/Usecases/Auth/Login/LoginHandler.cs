using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Application.Mappings;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ResponseDto<UserDto>>
{
  private readonly IUserRepository _userRepository;

  public LoginHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ResponseDto<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var account = await _userRepository.GetByEmailAsync(request.email, cancellationToken);

    if (account == null || request.password != account.Password)
    {
      return ResponseDto<UserDto>.Fail(ResponseStatusCode.NotFound, "incorrect email or password");
    }

    return ResponseDto<UserDto>.Success(ResponseStatusCode.OK, "data found", account.Map());
  }
}
