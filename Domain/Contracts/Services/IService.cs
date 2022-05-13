using Lombard.Domain.Contracts.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Domain.Contracts.Services
{
    public interface IService<T, TId>
    {
        Task<T> AddNew(T item);
        Task AddNewRange(IEnumerable<T> item);
        Task<IEnumerable<T>> GetALL(ISpecification<T> spec = null);
        Task<T> GetById(TId id);
        Task Remove(T item);
        Task RemoveRange(IEnumerable<T> list);
        Task Update(T item);
        Task UpdateRange(IEnumerable<T> list);
    }
}
