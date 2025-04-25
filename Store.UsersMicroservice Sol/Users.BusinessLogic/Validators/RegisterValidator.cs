using FluentValidation;
using Users.BusinessLogic.Dtos;

namespace Users.BusinessLogic.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(b => b.FirstName)
               .NotEmpty().WithMessage("The First Name field is required.")
               .MaximumLength(50).WithMessage("The Name field cannot exceed 50 characters.");

            RuleFor(b => b.LastName)
               .NotEmpty().WithMessage("The Last Name field is required.")
               .MaximumLength(50).WithMessage("The Name field cannot exceed 50 characters.");

            RuleFor(b => b.Email).NotEmpty().WithMessage("The Email field is required.")
                .EmailAddress().WithMessage("The Email field must be a valid email address.");

            RuleFor(b => b.Street).NotEmpty().WithMessage("The Street field is required.")
                .MaximumLength(50).WithMessage("The Street field cannot exceed 50 characters.");

            RuleFor(b => b.City).NotEmpty().WithMessage("The City field is required.")
                .MaximumLength(50).WithMessage("The City field cannot exceed 50 characters.");
            
            RuleFor(b => b.State).NotEmpty().WithMessage("The State field is required.")
                .MaximumLength(50).WithMessage("The State field cannot exceed 50 characters.");

            RuleFor(b => b.PostalCode).NotEmpty().WithMessage("The PostalCode field is required.")
                .Matches(@"^\d{5}(-\d{4})?$").WithMessage("The PostalCode field must be a valid ZIP code.");


        }
    }
}
