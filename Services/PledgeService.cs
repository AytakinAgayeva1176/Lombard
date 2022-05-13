using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class PledgeService: IPledgeService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PledgeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Pledge> AddNew(Pledge item)
        {
            return await _repositoryFactory.Repository.Add<Pledge>(item);
        }

        public Task AddNewRange(IEnumerable<Pledge> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pledge>> GetALL(ISpecification<Pledge> spec = null)
        {
            return await _repositoryFactory.Repository.List<Pledge>(spec);
        }

        public async Task<Pledge> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Pledge>(id);
        }

        public async Task Remove(Pledge item)
        {
            await _repositoryFactory.Repository.Delete<Pledge>(item);
        }

        public async Task RemoveRange(IEnumerable<Pledge> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Pledge>(list);
        }

        public async Task Update(Pledge item)
        {
            await _repositoryFactory.Repository.Update<Pledge>(item);
        }

        public async Task UpdateRange(IEnumerable<Pledge> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Pledge>(list);
        }
    }
}
