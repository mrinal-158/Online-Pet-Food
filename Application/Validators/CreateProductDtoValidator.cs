using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.Consumers)
                .NotEmpty().WithMessage("Consumers is required.");
            RuleFor(x => x.Price)
                .NotEmpty().When(x => x.Price <= 0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.Stock)
                .NotEmpty().When(x => x.Stock < 0).WithMessage("Stock cannot be negative.");
        }
    }
}
