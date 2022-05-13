using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class ActIndividualContractService : IActIndividualContractService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ActIndividualContractService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<ActIndividualContract> AddNew(ActIndividualContract item)
        {
            return await _repositoryFactory.Repository.Add<ActIndividualContract>(item);
        }

        public async Task AddNewRange(IEnumerable<ActIndividualContract> list)
        {
            await _repositoryFactory.Repository.AddRange<ActIndividualContract>(list);
        }

        public async Task<IEnumerable<ActIndividualContract>> GetALL(ISpecification<ActIndividualContract> spec = null)
        {
            return await _repositoryFactory.Repository.List<ActIndividualContract>(spec);
        }

        public async Task<ActIndividualContract> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<ActIndividualContract>(id);
        }

        public async Task Remove(ActIndividualContract item)
        {
            await _repositoryFactory.Repository.Delete<ActIndividualContract>(item);
        }

        public async Task RemoveRange(IEnumerable<ActIndividualContract> list)
        {
            await _repositoryFactory.Repository.DeleteRange<ActIndividualContract>(list);
        }

        public async Task Update(ActIndividualContract item)
        {
            await _repositoryFactory.Repository.Update<ActIndividualContract>(item);
        }

        public async Task UpdateRange(IEnumerable<ActIndividualContract> list)
        {
            await _repositoryFactory.Repository.UpdateRange<ActIndividualContract>(list);
        }
    }
}
