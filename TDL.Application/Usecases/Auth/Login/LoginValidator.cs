using FluentValidation;

namespace TDL.Application.Usecases.Auth.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
  public LoginValidator()
  {
    RuleFor(x => x.email)
      .NotEmpty()
      .EmailAddress()
      .WithMessage("required email");

    RuleFor(x => x.password)
      .NotEmpty()
      .WithMessage("required password");
  }
}
