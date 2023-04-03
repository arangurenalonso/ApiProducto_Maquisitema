namespace Application.Features.Products.Commands.UpdateProduct
{
    using Application.Features.Products.Vms;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class UpdateProductCommand : IRequest<ProductDTO>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public decimal DiscontPercent { get; set; }

    }
}
