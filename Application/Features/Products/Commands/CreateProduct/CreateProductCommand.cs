namespace Application.Features.Products.Commands.CreateProduct
{
    using Application.Features.Products.Vms;
    using MediatR;
    public class CreateProductCommand : IRequest<ProductDTO>
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public decimal DiscontPercent { get; set; }

    }
}
