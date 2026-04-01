using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be required.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must be required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone must be required.")
                .Matches(@"^\+?8801[3-9]\d{8}$").WithMessage("Phone must be a valid phone number (+8801[3-9]XXXXXXXX).");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must be required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}
