using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class LegalDebtorService :ILegalDebtorService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public LegalDebtorService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<LegalDebtor> AddNew(LegalDebtor item)
        {
            return await _repositoryFactory.Repository.Add<LegalDebtor>(item);
        }

        public Task AddNewRange(IEnumerable<LegalDebtor> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LegalDebtor>> GetALL(ISpecification<LegalDebtor> spec = null)
        {
            return await _repositoryFactory.Repository.List<LegalDebtor>(spec);
        }

        public async Task<LegalDebtor> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<LegalDebtor>(id);
        }

        public async Task Remove(LegalDebtor item)
        {
            await _repositoryFactory.Repository.Delete<LegalDebtor>(item);
        }

        public async Task RemoveRange(IEnumerable<LegalDebtor> list)
        {
            await _repositoryFactory.Repository.DeleteRange<LegalDebtor>(list);
        }

        public async Task Update(LegalDebtor item)
        {
            await _repositoryFactory.Repository.Update<LegalDebtor>(item);
        }

        public async Task UpdateRange(IEnumerable<LegalDebtor> list)
        {
            await _repositoryFactory.Repository.UpdateRange<LegalDebtor>(list);
        }
    }
}
