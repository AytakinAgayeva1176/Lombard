using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class IndividualContractService : IIndividualContractService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public IndividualContractService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IndividualContract> AddNew(IndividualContract item)
        {
            return await _repositoryFactory.Repository.Add<IndividualContract>(item);
        }

        public async Task AddNewRange(IEnumerable<IndividualContract> list)
        {
            await _repositoryFactory.Repository.AddRange<IndividualContract>(list);
        }

        public async Task<IEnumerable<IndividualContract>> GetALL(ISpecification<IndividualContract> spec = null)
        {
            return await _repositoryFactory.Repository.List<IndividualContract>(spec);
        }

        public async Task<IndividualContract> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<IndividualContract>(id);
        }

        public async Task Remove(IndividualContract item)
        {
            await _repositoryFactory.Repository.Delete<IndividualContract>(item);
        }

        public async Task RemoveRange(IEnumerable<IndividualContract> list)
        {
            await _repositoryFactory.Repository.DeleteRange<IndividualContract>(list);
        }

        public async Task Update(IndividualContract item)
        {
            await _repositoryFactory.Repository.Update<IndividualContract>(item);
        }

        public async Task UpdateRange(IEnumerable<IndividualContract> list)
        {
            await _repositoryFactory.Repository.UpdateRange<IndividualContract>(list);
        }
    }
}
