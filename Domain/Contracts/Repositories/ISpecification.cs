using System;
using System.Linq.Expressions;

namespace Lombard.Domain.Contracts.Repositories
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
