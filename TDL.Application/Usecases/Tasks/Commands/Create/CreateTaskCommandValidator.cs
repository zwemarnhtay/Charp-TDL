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
      .Must(d => d is DateTime)
      .WithMessage("Deadline is required and must be date time");

    RuleFor(task => task.UserId)
      .NotEmpty()
      .WithMessage("UserId is required");
  }
}
