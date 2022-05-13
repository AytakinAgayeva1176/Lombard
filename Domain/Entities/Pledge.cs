using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{

    // Korporativ müqavilə girovları
  
    public class Pledge: BaseEntity
    {
        public string ClientFullName { get; set; }
        public string Code { get; set; }
        public string PackageNumber { get; set; }

        
        public double? TotalWeight_375 { get; set; }
        public double? NetWeight_375 { get; set; }

        
        public double? TotalWeight_500 { get; set; }
        public double? NetWeight_500 { get; set; }

        
        public double? TotalWeight583_585 { get; set; }
        public double? NetWeight583_585 { get; set; }

        
        public double? TotalWeight_750 { get; set; }
        public double? NetWeight_750 { get; set; }

        
        public double? TotalWeight_875 { get; set; }
        public double? NetWeight_875 { get; set; }

        
        public double? TotalWeight_916 { get; set; }
        public double? NetWeight_916 { get; set; }

        
        public double? TotalWeight_999 { get; set; }
        public double? NetWeight_999 { get; set; }

        
        public double? TotalWeight { get; set; }
        public double? NetWeight { get; set; }
        
        public double? LikvidPrice { get; set; }
        public double? StorePrice { get; set; }

        public string AdditionalPledgeContractNumber { get; set; }
        public long PledgeContractId { get; set; }
        public PledgeContract PledgeContract { get; set; }
        public long ExtractionContractId { get; set; }
    }
}
