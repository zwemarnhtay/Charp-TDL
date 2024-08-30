using MediatR;
using TDL.Application.DTOs;
using TDL.Application.Interfaces.Repositories;
using TDL.Domain.Entities;
using TDL.Domain.Enums;

namespace TDL.Application.Usecases.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, ResponseDto>
{
  private readonly IRepository<UserEntity> _repository;
  private readonly IUserRepository _userRepository;

  public RegisterHandler(IRepository<UserEntity> repository, IUserRepository userRepository)
  {
    _repository = repository;
    _userRepository = userRepository;
  }

  public async Task<ResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var isExisted = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

    if (isExisted != null)
    {
      return new ResponseDto
      {
        StatusCode = ResponseStatusCode.Conflict,
        IsSuccess = false,
        Message = "this email has already registered"
      };
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
      return new ResponseDto
      {
        StatusCode = ResponseStatusCode.BadRequest,
        IsSuccess = true,
        Message = "falied to register new account"
      };
    }

    return new ResponseDto
    {
      StatusCode = ResponseStatusCode.OK,
      IsSuccess = true,
      Message = "success registeration"
    };
  }
}
