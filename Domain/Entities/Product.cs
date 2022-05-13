using Lombard.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{

    // Məhsul
    public class Product :BaseEntity
    {
        public long? GroupId { get; set; }
        public string Name { get; set; }
        public bool LocalCardAvailability { get; set; }
        public double? PenaltyPercentage { get; set; } // Cerime faizi
        public string ProductPurpose { get; set; } //Məhsulun məqsədi
        public string LoanAssignment { get; set; }//Kreditin təyinatı
        public string ProductType { get; set; } = "Lombard"; //Məhsulun Tipi
        public ProductGroup Group { get; set; }
    }


}
