using Lombard.Domain.Entities;
using Lombard.Domain.ViewModels;
using System.Threading.Tasks;

namespace Lombard.Domain.Contracts.Services
{
    public interface IActService : IService<Act, long>
    {
        public Task<ActDetailsVM> GetActWithAllInfo(long id);
    }
}
