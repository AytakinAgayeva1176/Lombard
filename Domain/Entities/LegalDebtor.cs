using Lombard.Domain.Enums;
using System.Collections.Generic;

namespace Lombard.Domain.Entities
{
    // Hüquqi borcalan
    public class LegalDebtor : Person
    {
        public string HeadAccountant { get; set; }
        public string DirectorName { get; set; }
        public string BusinessArea { get; set; }
        public ICollection<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public ICollection<Act> Acts { get; set; } = new List<Act>();
        public ICollection<CorporativeContract> CorporativeContracts { get; set; }=new List<CorporativeContract>();
        public ICollection<IndividualContract> IndividualContracts { get; set; } = new List<IndividualContract>();
        public ICollection<GuaranterContract> GuaranterIndividualContracts { get; set; }= new List<GuaranterContract>();    
    }
}

