namespace Application.Features.Products.Queries.GetProductById
{
    using Application.Features.Products.Vms;
    using MediatR;
    public class GetProductByIdQuery : IRequest<ProductByIdDTO>
    {
        public int ProductId { get; set; }
        public GetProductByIdQuery(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentNullException(nameof(productId)) : productId;
        }
    }
}
