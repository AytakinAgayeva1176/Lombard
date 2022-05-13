
using Lombard.Domain.Contracts.Repositories;
using Lombard.Domain.Contracts.Services;
using Lombard.Domain.Entities;
using Lombard.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Lombard.Services
{
    public class GuaranterIndividualContractService : IGuaranterIndividualContractService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public GuaranterIndividualContractService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<GuaranterContract> AddNew(GuaranterContract item)
        {
            return await _repositoryFactory.Repository.Add<GuaranterContract>(item);
        }

        public async Task AddNewRange(IEnumerable<GuaranterContract> list)
        {
            await _repositoryFactory.Repository.AddRange<GuaranterContract>(list);
        }

        public async Task<IEnumerable<GuaranterContract>> GetALL(ISpecification<GuaranterContract> spec = null)
        {
            return await _repositoryFactory.Repository.List<GuaranterContract>(spec);
        }

        public async Task<GuaranterContract> GetById(long id)
        {
            return await _repositoryFactory.Repository.GetById<GuaranterContract>(id);
        }

        public async Task Remove(GuaranterContract item)
        {
            await _repositoryFactory.Repository.Delete<GuaranterContract>(item);
        }

        public async Task RemoveRange(IEnumerable<GuaranterContract> list)
        {
            await _repositoryFactory.Repository.DeleteRange<GuaranterContract>(list);
        }

        public async Task Update(GuaranterContract item)
        {
            await _repositoryFactory.Repository.Update<GuaranterContract>(item);
        }

        public async Task UpdateRange(IEnumerable<GuaranterContract> list)
        {
            await _repositoryFactory.Repository.UpdateRange<GuaranterContract>(list);
        }
    }
}
