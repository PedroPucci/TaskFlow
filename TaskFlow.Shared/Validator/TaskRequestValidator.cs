using FluentValidation;
using TaskFlow.Domain.Entity;
using TaskFlow.Shared.Enums.Error;
using TaskFlow.Shared.Helpers;

namespace TaskFlow.Shared.Validator
{
    public class TaskRequestValidator : AbstractValidator<TaskEntity>
    {
        public TaskRequestValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage(TaskErrors.Task_Error_TitleCanNotBeNullOrEmpty.Description())
                .MinimumLength(4)
                    .WithMessage(TaskErrors.Task_Error_TitleLenghtLessFour.Description());

            RuleFor(p => p.Description)
                .NotEmpty()
                    .WithMessage(TaskErrors.Task_Error_DescriptionCanNotBeNullOrEmpty.Description())
                .MinimumLength(4)
                    .WithMessage(TaskErrors.Task_Error_DescriptionLenghtLessFour.Description());
        }
    }
}