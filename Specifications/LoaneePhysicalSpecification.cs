using Lombard.Domain.Entities;
using Lombard.Repositories;

namespace Lombard.Specifications
{
    public class IndividualDebtorSpecification : BaseSpecification<IndividualDebtor>
    {
        public IndividualDebtorSpecification(long id)
        {
            Criteria = i => i.Id == id;
        }
    }
}
