using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class BankAccountService:IBankAccountService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public BankAccountService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<BankAccount> AddNew(BankAccount item)
        {
            return await _repositoryFactory.Repository.Add<BankAccount>(item);
        }

        public async Task AddNewRange(IEnumerable<BankAccount> list)
        {
            await _repositoryFactory.Repository.AddRange<BankAccount>(list);
        }

        public async Task<IEnumerable<BankAccount>> GetALL(ISpecification<BankAccount> spec = null)
        {
            return await _repositoryFactory.Repository.List<BankAccount>(spec);
        }

        public async Task<BankAccount> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<BankAccount>(id);
        }

        public async Task Remove(BankAccount item)
        {
            await _repositoryFactory.Repository.Delete<BankAccount>(item);
        }

        public async Task RemoveRange(IEnumerable<BankAccount> list)
        {
            await _repositoryFactory.Repository.DeleteRange<BankAccount>(list);
        }

        public async Task Update(BankAccount item)
        {
            await _repositoryFactory.Repository.Update<BankAccount>(item);
        }

        public async Task UpdateRange(IEnumerable<BankAccount> list)
        {
            await _repositoryFactory.Repository.UpdateRange<BankAccount>(list);
        }
    }
}
