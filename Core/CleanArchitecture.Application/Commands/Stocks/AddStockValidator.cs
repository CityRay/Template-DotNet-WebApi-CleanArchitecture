using FluentValidation;

namespace CleanArchitecture.Application.Commands.Stocks
{
    public class AddStockValidator : AbstractValidator<AddStockRequest>
    {
        public AddStockValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Symbol).NotEmpty().ChildRules(x => x.RuleFor(y => y.Length).GreaterThanOrEqualTo(4).WithMessage("輸入正確代碼"));
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required").GreaterThan(-1);
        }

    }
}
