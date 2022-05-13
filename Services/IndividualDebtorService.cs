using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Domain.ViewModels;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class IndividualDebtorService : IIndividualDebtorService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public IndividualDebtorService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IndividualDebtor> AddNew(IndividualDebtor item)
        {
            return await _repositoryFactory.Repository.Add<IndividualDebtor>(item);
        }

        public Task AddNewRange(IEnumerable<IndividualDebtor> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IndividualDebtor>> GetALL(ISpecification<IndividualDebtor> spec = null)
        {
            return await _repositoryFactory.Repository.List<IndividualDebtor>(spec);
        }

        public async Task<IndividualDebtor> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<IndividualDebtor>(id);
        }

        //public async Task<IndividualDebtorWithALLInfo> GetIndividualDebtorWithALLInfo(long id)
        //{
        //    return await _repositoryFactory.Repository.GetById<IndividualDebtorWithALLInfo>(id);
        //}

        public async Task Remove(IndividualDebtor item)
        {
            await _repositoryFactory.Repository.Delete<IndividualDebtor>(item);
        }

        public async Task RemoveRange(IEnumerable<IndividualDebtor> list)
        {
            await _repositoryFactory.Repository.DeleteRange<IndividualDebtor>(list);
        }

        public async Task Update(IndividualDebtor item)
        {
            await _repositoryFactory.Repository.Update<IndividualDebtor>(item);
        }

        public async Task UpdateRange(IEnumerable<IndividualDebtor> list)
        {
            await _repositoryFactory.Repository.UpdateRange<IndividualDebtor>(list);
        }
    }
}
