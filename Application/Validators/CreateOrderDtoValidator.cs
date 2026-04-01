using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator() 
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.");
                
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrderItemsDto)
                .NotNull()
                .WithMessage("Order items are required")
                .Must(items => items != null && items.Count > 0 && items.All(item => item.Amount > 0))
                .WithMessage("At least one order item is required and All order items must have an amount greater than 0.")
                .Must(HaveDistinctProductIds)
                .WithMessage("Duplicate product are not allowed. Each product can only be ordered once for each order.");

            RuleForEach(x => x.OrderItemsDto)
                .SetValidator(new OrderItemDtoValidator());
        }

        private static bool HaveDistinctProductIds(List<OrderItemDto> orderItems)
        {
            var productIds = orderItems.Select(x => x.ProductId).ToList();
            return productIds.Distinct().Count() == productIds.Count;
        }
    }
}
