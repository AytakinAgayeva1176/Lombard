using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class BusinessAreaService : IBusinessAreaService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public BusinessAreaService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<BusinessArea> AddNew(BusinessArea item)
        {
            return await _repositoryFactory.Repository.Add<BusinessArea>(item);
        }

        public Task AddNewRange(IEnumerable<BusinessArea> item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessArea>> GetALL(ISpecification<BusinessArea> spec = null)
        {
            return await _repositoryFactory.Repository.List<BusinessArea>(spec);
        }

        public async Task<BusinessArea> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<BusinessArea>(id);
        }

        public async Task Remove(BusinessArea item)
        {
            await _repositoryFactory.Repository.Delete<BusinessArea>(item);
        }

        public async Task RemoveRange(IEnumerable<BusinessArea> list)
        {
            await _repositoryFactory.Repository.DeleteRange<BusinessArea>(list);
        }

        public async Task Update(BusinessArea item)
        {
            await _repositoryFactory.Repository.Update<BusinessArea>(item);
        }

        public async Task UpdateRange(IEnumerable<BusinessArea> list)
        {
            await _repositoryFactory.Repository.UpdateRange<BusinessArea>(list);
        }
    }
}
