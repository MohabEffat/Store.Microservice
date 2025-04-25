using FluentValidation;
using Products.BusinessLogic.Dtos;

namespace Products.BusinessLogic.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {

        }
    }
}
