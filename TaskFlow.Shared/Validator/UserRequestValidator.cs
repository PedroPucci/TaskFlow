using FluentValidation;
using TaskFlow.Domain.Entity;
using TaskFlow.Shared.Enums.Error;
using TaskFlow.Shared.Helpers;

namespace TaskFlow.Shared.Validator
{
    public class UserRequestValidator : AbstractValidator<UserEntity>
    {
        public UserRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_NameCanNotBeNullOrEmpty.Description())
                .MinimumLength(4)
                    .WithMessage(UserErrors.User_Error_NameLenghtLessFour.Description());

            RuleFor(p => p.Email)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_EmailCanNotBeNullOrEmpty.Description())
                .MinimumLength(4)
                    .WithMessage(UserErrors.User_Error_EmailLenghtLessFour.Description())
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                        .WithMessage(UserErrors.User_Error_InvalidEmailFormat.Description());
            RuleFor(p => p.Password)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_PasswordCanNotBeNullOrEmpty.Description())
                .MinimumLength(6)
                    .WithMessage(UserErrors.User_Error_PasswordLenghtLessFour.Description())
                .Matches(@"^(?=.*[A-Za-z]{4,})(?=.*\d{2,}).*$")
                    .WithMessage(UserErrors.User_Error_PasswordInvalid.Description());
        }
    }
}