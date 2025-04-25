using FluentValidation;
using Users.BusinessLogic.Dtos;

namespace Users.BusinessLogic.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
             RuleFor(b => b.Email).NotEmpty().WithMessage("The Email field is required.")
                .EmailAddress().WithMessage("The Email field must be a valid email address.");

        }
    }
}
