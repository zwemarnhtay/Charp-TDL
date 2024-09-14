using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Auth.Login;

public record LoginCommand(string email, string password) : IRequest<ResponseDto<UserDto>>;
