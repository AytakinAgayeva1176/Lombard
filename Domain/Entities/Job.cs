using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{
    // Fizki borcalan iş yeri
    public class Job:BaseEntity
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string VOEN { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Salary { get; set; }
        public double? Experience { get; set; }
        public string Adress { get; set; }
        public bool IsCurrent { get; set; }
        public long? IndividualDebtorId { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }

    }
}
