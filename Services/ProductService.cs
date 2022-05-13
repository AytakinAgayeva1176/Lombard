using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ProductService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Product> AddNew(Product item)
        {
            return await _repositoryFactory.Repository.Add<Product>(item);
        }

        public async Task AddNewRange(IEnumerable<Product> list)
        {
            await _repositoryFactory.Repository.AddRange<Product>(list);
        }

        public async Task<IEnumerable<Product>> GetALL(ISpecification<Product> spec = null)
        {
            return await _repositoryFactory.Repository.List<Product>(spec);
        }

        public async Task<Product> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Product>(id);
        }

        public async Task Remove(Product item)
        {
            await _repositoryFactory.Repository.Delete<Product>(item);
        }

        public async Task RemoveRange(IEnumerable<Product> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Product>(list);
        }

        public async Task Update(Product item)
        {
            await _repositoryFactory.Repository.Update<Product>(item);
        }

        public async Task UpdateRange(IEnumerable<Product> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Product>(list);
        }
    }
}
