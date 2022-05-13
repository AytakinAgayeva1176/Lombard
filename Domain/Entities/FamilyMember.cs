using Lombard.Domain.Enums;
using System;

namespace Lombard.Domain.Entities
{
    // Ailə üzvü
    public class FamilyMember : BaseEntity
    {
        public string Fullname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        public double? Salary { get; set; }
        public string MobileNumber { get; set; }
        public string MobileAdditional { get; set; }
        public string Email { get; set; }
        public long RelationTypeId { get; set; }
        public string RelationType { get; set; }
        public long? IndividualDebtorId { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }
        public long? LegalDebtorId { get; set; }
        public LegalDebtor LegalDebtor { get; set; }

    }
}
