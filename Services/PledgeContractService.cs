using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class PledgeContractService: IPledgeContractService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PledgeContractService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<PledgeContract> AddNew(PledgeContract item)
        {
            return await _repositoryFactory.Repository.Add<PledgeContract>(item);
        }

        public Task AddNewRange(IEnumerable<PledgeContract> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PledgeContract>> GetALL(ISpecification<PledgeContract> spec = null)
        {
            return await _repositoryFactory.Repository.List<PledgeContract>(spec);
        }

        public async Task<PledgeContract> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<PledgeContract>(id);
        }

        public async Task Remove(PledgeContract item)
        {
            await _repositoryFactory.Repository.Delete<PledgeContract>(item);
        }

        public async Task RemoveRange(IEnumerable<PledgeContract> list)
        {
            await _repositoryFactory.Repository.DeleteRange<PledgeContract>(list);
        }

        public async Task Update(PledgeContract item)
        {
            await _repositoryFactory.Repository.Update<PledgeContract>(item);
        }

        public async Task UpdateRange(IEnumerable<PledgeContract> list)
        {
            await _repositoryFactory.Repository.UpdateRange<PledgeContract>(list);
        }
    }
}
