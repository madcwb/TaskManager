using FluentValidation;

namespace TaskManager.Models
{
    public class TaskListValidator : AbstractValidator<TaskList>
    {
        public TaskListValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(50).WithMessage("O título deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatório.")
                .MaximumLength(1000).WithMessage("A descrição deve ter no máximo 1000 caracteres.");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Prioridade obrigatório.")
                .GreaterThanOrEqualTo(1).WithMessage("A prioridade deve ser maior ou igual a 1")
                .LessThanOrEqualTo(3).WithMessage("A prioridade deve ser menor ou igual a 3");
        }
    }
}
