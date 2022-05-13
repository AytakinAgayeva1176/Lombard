using Lombard.Domain.Entities;
using Lombard.Domain.ViewModels;
using System.Threading.Tasks;

namespace Lombard.Domain.Contracts.Services
{
    public interface ICorporativeContractService : IService<CorporativeContract, long>
    {
        public Task<CorporativeContractDetailsVM> GetCorporativeContractWithAllInfo(long id);
    }
}
