using FluentValidation;

namespace TDL.Application.Usecases.Tasks.Commands.Edit;

public class EditTaskValidator : AbstractValidator<EditTaskCommand>
{
  public EditTaskValidator()
  {
    RuleFor(task => task.Id).NotEmpty();
    RuleFor(task => task.Title).NotEmpty();
    RuleFor(task => task.Description).NotEmpty();
    RuleFor(task => task.Deadline).NotEmpty();
    RuleFor(task => task.IsCompleted).NotEmpty();
    RuleFor(task => task.UserId).NotEmpty();
  }
}
