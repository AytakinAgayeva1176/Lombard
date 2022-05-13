using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class JobService : IJobService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public JobService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Job> AddNew(Job item)
        {
            return await _repositoryFactory.Repository.Add<Job>(item);
        }

        public Task AddNewRange(IEnumerable<Job> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetALL(ISpecification<Job> spec = null)
        {
            return await _repositoryFactory.Repository.List<Job>(spec);
        }

        public async Task<Job> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Job>(id);
        }

        public async Task Remove(Job item)
        {
            await _repositoryFactory.Repository.Delete<Job>(item);
        }

        public async Task RemoveRange(IEnumerable<Job> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Job>(list);
        }

        public async Task Update(Job item)
        {
            await _repositoryFactory.Repository.Update<Job>(item);
        }

        public async Task UpdateRange(IEnumerable<Job> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Job>(list);
        }
    }
}
