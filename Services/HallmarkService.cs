using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class HallmarkService : IHallmarkService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public HallmarkService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Hallmark> AddNew(Hallmark item)
        {
            return await _repositoryFactory.Repository.Add<Hallmark>(item);
        }

        public async Task AddNewRange(IEnumerable<Hallmark> list)
        {
            await _repositoryFactory.Repository.AddRange<Hallmark>(list);
        }

        public async Task<IEnumerable<Hallmark>> GetALL(ISpecification<Hallmark> spec = null)
        {
            return await _repositoryFactory.Repository.List<Hallmark>(spec);
        }

        public async Task<Hallmark> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Hallmark>(id);
        }

        public async Task Remove(Hallmark item)
        {
            await _repositoryFactory.Repository.Delete<Hallmark>(item);
        }

        public async Task RemoveRange(IEnumerable<Hallmark> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Hallmark>(list);
        }

        public async Task Update(Hallmark item)
        {
            await _repositoryFactory.Repository.Update<Hallmark>(item);
        }

        public async Task UpdateRange(IEnumerable<Hallmark> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Hallmark>(list);
        }
    }
}
