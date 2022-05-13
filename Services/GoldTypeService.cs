using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class GoldTypeService : IGoldTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public GoldTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<GoldType> AddNew(GoldType item)
        {
            return await _repositoryFactory.Repository.Add<GoldType>(item);
        }

        public async Task AddNewRange(IEnumerable<GoldType> list)
        {
            await _repositoryFactory.Repository.AddRange<GoldType>(list);
        }

        public async Task<IEnumerable<GoldType>> GetALL(ISpecification<GoldType> spec = null)
        {
            return await _repositoryFactory.Repository.List<GoldType>(spec);
        }

        public async Task<GoldType> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<GoldType>(id);
        }

        public async Task Remove(GoldType item)
        {
            await _repositoryFactory.Repository.Delete<GoldType>(item);
        }

        public async Task RemoveRange(IEnumerable<GoldType> list)
        {
            await _repositoryFactory.Repository.DeleteRange<GoldType>(list);
        }

        public async Task Update(GoldType item)
        {
            await _repositoryFactory.Repository.Update<GoldType>(item);
        }

        public async Task UpdateRange(IEnumerable<GoldType> list)
        {
            await _repositoryFactory.Repository.UpdateRange<GoldType>(list);
        }
    }
}
