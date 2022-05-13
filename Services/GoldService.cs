
using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class GoldService : IGoldService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public GoldService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Gold> AddNew(Gold item)
        {
            return await _repositoryFactory.Repository.Add<Gold>(item);
        }

        public async Task AddNewRange(IEnumerable<Gold> list)
        {
            await _repositoryFactory.Repository.AddRange<Gold>(list);
        }

        public async Task<IEnumerable<Gold>> GetALL(ISpecification<Gold> spec = null)
        {
            return await _repositoryFactory.Repository.List<Gold>(spec);
        }

        public async Task<Gold> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Gold>(id);
        }

        public async Task Remove(Gold item)
        {
            await _repositoryFactory.Repository.Delete<Gold>(item);
        }

        public async Task RemoveRange(IEnumerable<Gold> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Gold>(list);
        }

        public async Task Update(Gold item)
        {
            await _repositoryFactory.Repository.Update<Gold>(item);
        }

        public async Task UpdateRange(IEnumerable<Gold> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Gold>(list);
        }
    }
}
