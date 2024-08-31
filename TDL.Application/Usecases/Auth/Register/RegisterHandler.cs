using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, ResponseDto<UserDto>>
{
  private readonly IRepository<UserEntity> _repository;
  private readonly IUserRepository _userRepository;

  public RegisterHandler(IRepository<UserEntity> repository, IUserRepository userRepository)
  {
    _repository = repository;
    _userRepository = userRepository;
  }

  public async Task<ResponseDto<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var isExisted = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

    if (isExisted != null)
    {
      return ResponseDto<UserDto>.Fail(ResponseStatusCode.Conflict, "this email has already registered");
    }

    var user = new UserEntity
    {
      Id = Guid.NewGuid().ToString(),
      Name = request.Name,
      Email = request.Email,
      Password = request.Password,
    };

    var result = await _repository.CreateAsync(user, cancellationToken);

    if (result is Result.failed)
    {
      return ResponseDto<UserDto>.Fail(ResponseStatusCode.BadRequest, "failed to register new account");
    }

    return ResponseDto<UserDto>.Success(ResponseStatusCode.OK, "registered success");
  }
}
