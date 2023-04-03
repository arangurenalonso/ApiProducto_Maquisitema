namespace Application.Features.Products.Commands.CreateProduct
{
    using FluentValidation;
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El {PropertyName} del producto no puede estar en blanco")
                .MaximumLength(50).WithMessage("El {PropertyName} no puede exceder los 50 caracteres")
                .WithName("nombre"); ;

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La {PropertyName} no puede ser nula")
                .WithName("descripción"); ;


            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("El  {PropertyName} no puede ser nulo")
                .GreaterThanOrEqualTo(0).WithMessage("El  {PropertyName} no puede ser menor a 0")
                .WithName("stock"); ;

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("El {PropertyName} no puede ser nulo")
                .GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0")
                .WithName("Price");
            RuleFor(p => p.DiscontPercent)
                .NotEmpty().WithMessage("El {PropertyName} no puede ser nulo")
                .GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0")
                .LessThanOrEqualTo(100).WithMessage("El {PropertyName} debe ser menor a 100")
                .WithName("Discont Percent");

        }
    }
}
