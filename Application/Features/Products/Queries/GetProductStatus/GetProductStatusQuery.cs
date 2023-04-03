namespace Application.Features.Products.Queries.GetProductStatus
{
    using Application.Models;
    using MediatR;

    public class GetProductStatusQuery : IRequest<List<Status>>
    {
        public GetProductStatusQuery()
        {
        }
    }
}
