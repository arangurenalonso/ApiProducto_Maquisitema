namespace Application.Features.Products.Queries.GetProductList
{
    using Application.Features.Products.Vms;
    using MediatR;
    using System.Linq;
    public class GetProductListQuery : IRequest<IReadOnlyList<ProductDTO>>
    {
        public GetProductListQuery()
        {
        }
    }
}
