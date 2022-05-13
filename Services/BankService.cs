using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class BankService: IBankService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public BankService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Bank> AddNew(Bank item)
        {
            return await _repositoryFactory.Repository.Add<Bank>(item);
        }

        public Task AddNewRange(IEnumerable<Bank> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bank>> GetALL(ISpecification<Bank> spec = null)
        {
            return await _repositoryFactory.Repository.List<Bank>(spec);
        }

        public async Task<Bank> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<Bank>(id);
        }

        public async Task Remove(Bank item)
        {
            await _repositoryFactory.Repository.Delete<Bank>(item);
        }

        public async Task RemoveRange(IEnumerable<Bank> list)
        {
            await _repositoryFactory.Repository.DeleteRange<Bank>(list);
        }

        public async Task Update(Bank item)
        {
            await _repositoryFactory.Repository.Update<Bank>(item);
        }

        public async Task UpdateRange(IEnumerable<Bank> list)
        {
            await _repositoryFactory.Repository.UpdateRange<Bank>(list);
        }
    }
}
