namespace Application.Contracts
{
    using Application.Contracts.Persistence;
    using Domain.Common;
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
        public IProductRepository ProductRepository { get; }

    }
}
