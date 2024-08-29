using FluentValidation;

namespace TDL.Application.Usecases.Tasks.Commands.Create;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
  public CreateTaskCommandValidator()
  {
    RuleFor(task => task.Title)
      .NotEmpty()
      .WithMessage("Title is required");

    RuleFor(task => task.Deadline)
      .NotEmpty()
      .Must(d => d is DateOnly)
      .WithMessage("Deadline is required and must be date only");

    RuleFor(task => task.UserId)
      .NotEmpty()
      .WithMessage("UserId is required");
  }
}
