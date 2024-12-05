using FluentValidation;

namespace AppServices.Products.Update;

public class UpdateProductRequestValidator: AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi gereklidir")
                .Length(3, 10).WithMessage("Ürün ismi en az 3 en fazla 10 karakter olabilir");

            //price validator
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Ürün fiyatı gereklidir")
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");

            //stock validator
            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Ürün stok adedi gereklidir")
                .InclusiveBetween(1, 200).WithMessage("Ürün stok adedi 1 ile 200 arasında olmalıdır");

    }
    }

