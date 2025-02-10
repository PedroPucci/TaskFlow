using FluentValidation;
using TaskFlow.Domain.Entity;
using TaskFlow.Shared.Enums.Error;
using TaskFlow.Shared.Helpers;

namespace TaskFlow.Shared.Validator
{
    public class CategoryRequestValidator : AbstractValidator<CategoryEntity>
    {
        public CategoryRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage(CategoryErrors.Category_Error_NameCanNotBeNullOrEmpty.Description())
                .MinimumLength(4)
                    .WithMessage(CategoryErrors.Category_Error_NameLenghtLessFour.Description());
        }
    }
}