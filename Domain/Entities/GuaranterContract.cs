namespace Lombard.Domain.Entities
{
    public class GuaranterContract : BaseEntity
    {
        public long? IndividualDebtorId { get; set; }
        public long? LegalDebtorId { get; set; }
        public LegalDebtor LegalDebtor { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }
        public long? IndividualContractId { get; set; }
        public IndividualContract IndividualContract { get; set; }
        public long? CorporativeContractId { get; set; }
        public CorporativeContract CorporativeContract { get; set; }
    }
}
