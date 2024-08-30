using MediatR;
using TDL.Application.DTOs;

namespace TDL.Application.Usecases.Auth.Register;

public record RegisterCommand(string Name, string Email, string Password, string Repassword) : IRequest<ResponseDto>;

