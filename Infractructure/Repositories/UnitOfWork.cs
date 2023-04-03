using Application.Contracts;
using Application.Contracts.Persistence;
using Domain.Common;
using Infractructure.Persistence;
using System.Collections;

namespace Infractructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ApplicationDbContext _context;
        private IProductRepository _productRepository;

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error en Transacción ",e);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
