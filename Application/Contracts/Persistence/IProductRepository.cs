namespace Application.Contracts.Persistence
{
    using Domain.Entities;
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}