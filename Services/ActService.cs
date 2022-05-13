using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Domain.ViewModels;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class ActService :IActService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ActService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Act> AddNew(Act item)
        {
            return await _repositoryFactory.Repository.Add<Act>(item);
        }

        public async Task AddNewRange(IEnumerable<Act> list)
        {
            await _repositoryFactory.Repository.AddRange<Act>(list);
        }

        public async Task<IEnumerable<Act>> GetALL(ISpecification<Act> spec = null)
        {
            return await _repositoryFactory.Repository.List<Act>(spec);
        }

        public async Task<Act> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Act>(id);
        }

        public async Task<ActDetailsVM> GetActWithAllInfo(long id)
        {
            return await _repositoryFactory.Repository.GetById<ActDetailsVM>(id);
        }
        public async Task Remove(Act item)
        {
            await _repositoryFactory.Repository.Delete<Act>(item);
        }

        public async Task RemoveRange(IEnumerable<Act> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Act>(list);
        }

        public async Task Update(Act item)
        {
            await _repositoryFactory.Repository.Update<Act>(item);
        }

        public async Task UpdateRange(IEnumerable<Act> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Act>(list);
        }
    }
}
