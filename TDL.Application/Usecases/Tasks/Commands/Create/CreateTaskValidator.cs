using FluentValidation;

namespace TDL.Application.Usecases.Tasks.Commands.Create;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
  public CreateTaskValidator()
  {
    RuleFor(task => task.Title)
      .NotEmpty()
      .WithMessage("Title is required");

    RuleFor(task => task.Deadline)
      .NotEmpty()
      .WithMessage("Deadline is required and must be date time")
      .GreaterThanOrEqualTo(DateTime.Now)
      .WithMessage("Deadline must be in the future");

    RuleFor(task => task.UserId)
      .NotEmpty()
      .WithMessage("UserId is required");
  }
}
