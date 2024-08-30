using FluentValidation;

namespace TDL.Application.Usecases.Auth.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
  public RegisterValidator()
  {
    RuleFor(acc => acc.Name)
      .NotEmpty()
      .WithMessage("required Name");

    RuleFor(acc => acc.Email)
      .NotEmpty()
      .EmailAddress()
      .WithMessage("required Email address");

    RuleFor(acc => acc.Password)
      .NotEmpty()
      .WithMessage("required password")
      .MinimumLength(6)
      .WithMessage("must be at least 6 characters");

    RuleFor(acc => acc.Repassword)
      .NotEmpty()
      .WithMessage("required Password")
      .Equal(acc => acc.Password)
      .WithMessage("must be equal Password");

  }
}
