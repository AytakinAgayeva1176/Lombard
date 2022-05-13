using System.Collections.Generic;

namespace Lombard.Domain.Entities
{

    //  Korporativ Müqavilə
    public class CorporativeContract : Contract
    {
        public double CreditLimit { get; set; }//Kredit Limiti 
        public long? ParentId { get; set; }

        
        public ICollection<PledgeContract> PledgeContracts { get; set; } = new List<PledgeContract>();
        public ICollection<GuaranterContract> GuaranterContract { get; set; }
        public CorporativeContract Parent { get; set; }
    }
}
