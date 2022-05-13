using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{
    // Kredit ödəniş cədvəli
    public class PaymentSchedule:BaseEntity
    {
        public long? CorporativeContractID { get; set; }//Kredit ID	Müqavilə nömrəsi
        public long? IndividualContractID { get; set; }//Kredit ID	Müqavilə nömrəsi
        public DateTime PaymentDate { get; set; } //Ödəniş tarixi
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPayment { get; set; }//Cəm ödəniş
        [Column(TypeName = "decimal(18,4)")]
        public decimal InitialRemainder { get; set; }//Əvvələ qalıq
        [Column(TypeName = "decimal(18,4)")]
        public decimal LastRemainder { get; set; }//Son qalıq
        [Column(TypeName = "decimal(18,4)")]
        public decimal MainDebtPayment { get; set; }//Əsas borc üzrə ödəniş
        [Column(TypeName = "decimal(18,4)")]
        public decimal PercentageDebtPayment { get; set; }//Faiz borc üzrə ödəniş
        public CorporativeContract CorporativeContracts { get; set; } 
        public IndividualContract IndividualContracts { get; set; } 
    }
}

