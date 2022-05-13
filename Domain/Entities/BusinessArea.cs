using System.Collections.Generic;

namespace Lombard.Domain.Entities
{
    //Fəaliyyət sahəsi
    public class BusinessArea : BaseEntity
    {
        public string Area { get; set; }

       // public ICollection<LegalDebtor> LegalDebtors { get; set; } = new List<LegalDebtor>();
    }
}
