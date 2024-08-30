using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;

namespace TDL.Application.Usecases.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, UserDto>
{
  private readonly IUserRepository _userRepository;

  public LoginHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<UserDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var account = await _userRepository.GetByEmailAsync(request.email, cancellationToken);

    if (account == null || request.password != account.Password)
    {
      return null;
    }

    var dto = new UserDto(account.Id, account.Name, account.Email, account.Password);
    return dto;
  }
}
