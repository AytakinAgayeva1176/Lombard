
using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class RelationTypeService : IRelationTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public RelationTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<RelationType> AddNew(RelationType item)
        {
            return await _repositoryFactory.Repository.Add<RelationType>(item);
        }

        public async Task AddNewRange(IEnumerable<RelationType> list)
        {
            await _repositoryFactory.Repository.AddRange<RelationType>(list);
        }

        public async Task<IEnumerable<RelationType>> GetALL(ISpecification<RelationType> spec = null)
        {
            return await _repositoryFactory.Repository.List<RelationType>(spec);
        }

        public async Task<RelationType> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<RelationType>(id);
        }

        public async Task Remove(RelationType item)
        {
            await _repositoryFactory.Repository.Delete<RelationType>(item);
        }

        public async Task RemoveRange(IEnumerable<RelationType> list)
        {
            await _repositoryFactory.Repository.DeleteRange<RelationType>(list);
        }

        public async Task Update(RelationType item)
        {
            await _repositoryFactory.Repository.Update<RelationType>(item);
        }

        public async Task UpdateRange(IEnumerable<RelationType> list)
        {
            await _repositoryFactory.Repository.UpdateRange<RelationType>(list);
        }
    }
}
