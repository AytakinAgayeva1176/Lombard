namespace Lombard.Domain.Entities
{
    public class ActIndividualContract : BaseEntity
    {
        public long ActId { get; set; }
        public Act Act { get; set; }
        public long IndividualContractId { get; set; }
        public IndividualContract IndividualContract { get; set; }
    }
}
