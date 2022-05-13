using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Services
{
    public class FamilyMembersService : IFamilyMembersService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public FamilyMembersService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<FamilyMember> AddNew(FamilyMember item)
        {
            return await _repositoryFactory.Repository.Add<FamilyMember>(item);
        }

        public async Task AddNewRange(IEnumerable<FamilyMember> list)
        {
            await _repositoryFactory.Repository.AddRange<FamilyMember>(list);
        }

        public async Task<IEnumerable<FamilyMember>> GetALL(ISpecification<FamilyMember> spec = null)
        {
            return await _repositoryFactory.Repository.List<FamilyMember>(spec);
        }

        public async Task<FamilyMember> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<FamilyMember>(id);
        }

        public async Task Remove(FamilyMember item)
        {
            await _repositoryFactory.Repository.Delete<FamilyMember>(item);
        }

        public async Task RemoveRange(IEnumerable<FamilyMember> list)
        {
            await _repositoryFactory.Repository.DeleteRange<FamilyMember>(list);
        }

        public async Task Update(FamilyMember item)
        {
            await _repositoryFactory.Repository.Update<FamilyMember>(item);
        }

        public async Task UpdateRange(IEnumerable<FamilyMember> list)
        {
            await _repositoryFactory.Repository.UpdateRange<FamilyMember>(list);
        }
    }
}
