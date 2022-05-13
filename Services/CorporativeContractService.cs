using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Domain.ViewModels;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class CorporativeContractService : ICorporativeContractService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CorporativeContractService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<CorporativeContract> AddNew(CorporativeContract item)
        {
            return await _repositoryFactory.Repository.Add<CorporativeContract>(item);
        }

        public async Task AddNewRange(IEnumerable<CorporativeContract> list)
        {
            await _repositoryFactory.Repository.AddRange<CorporativeContract>(list);
        }

        public async Task<IEnumerable<CorporativeContract>> GetALL(ISpecification<CorporativeContract> spec = null)
        {
            return await _repositoryFactory.Repository.List<CorporativeContract>(spec);
        }

        public async Task<CorporativeContract> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<CorporativeContract>(id);
        }
        public async Task<CorporativeContractDetailsVM> GetCorporativeContractWithAllInfo(long id)
        {
            return await _repositoryFactory.Repository.GetById<CorporativeContractDetailsVM>(id);
        }
        public async Task Remove(CorporativeContract item)
        {
            await _repositoryFactory.Repository.Delete<CorporativeContract>(item);
        }

        public async Task RemoveRange(IEnumerable<CorporativeContract> list)
        {
            await _repositoryFactory.Repository.DeleteRange<CorporativeContract>(list);
        }

        public async Task Update(CorporativeContract item)
        {
            await _repositoryFactory.Repository.Update<CorporativeContract>(item);
        }

        public async Task UpdateRange(IEnumerable<CorporativeContract> list)
        {
            await _repositoryFactory.Repository.UpdateRange<CorporativeContract>(list);
        }
    }
}
