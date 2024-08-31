using FluentValidation;

namespace TDL.Application.Usecases.Tasks.Commands.Edit;

public class EditTaskValidator : AbstractValidator<EditTaskCommand>
{
  public EditTaskValidator()
  {
    RuleFor(task => task.Id)
      .NotEmpty()
      .WithMessage("required id");

    RuleFor(task => task.Title)
      .NotEmpty()
      .WithMessage("required title"); ;

    RuleFor(task => task.Deadline)
      .NotEmpty()
      .WithMessage("required deadline"); ;

    RuleFor(task => task.IsCompleted)
      .NotEmpty()
      .WithMessage("required complete status"); ;

    RuleFor(task => task.UserId)
      .NotEmpty()
      .WithMessage("required user id"); ;
  }
}
