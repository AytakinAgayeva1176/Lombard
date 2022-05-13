using Lombard.Domain.Entities;
using System.Collections.Generic;

namespace Lombard.Domain.ViewModels
{
    public class CorporativeContractDetailsVM : CorporativeContract
    {
        public ICollection<CorporativeContract> AdditionalContracts { get; set; } = new List<CorporativeContract>();
        public ICollection<PledgeContract> AddedPledgesContract { get; set; }= new List<PledgeContract>();
        public ICollection<PledgeContract> ExtractedPledgesContract { get; set; }=new List<PledgeContract>();
        public  Act FinalPledges { get; set; }
    }
}
