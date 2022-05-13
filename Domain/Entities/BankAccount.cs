using System;

namespace Lombard.Domain.Entities
{
    //Borcalanin bank hesabi
    public class BankAccount : BaseEntity
    {
        public string Code { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public long BankId { get; set; }
        public long? IndividualDebtorId { get; set; }
        public long? LegalDebtorId { get; set; }
        public bool IsMainAccount { get; set; }

        public Bank Bank { get; set; }

        public LegalDebtor LegalDebtor { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }
    }
}
