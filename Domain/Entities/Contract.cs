using Lombard.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{

    //Müqavilələrin ortaq field-ləri
    public class Contract : BaseEntity
    {
        public long? IndividualDebtorId { get; set; } //Borcalan	Kontragent ID		
        public long? LegalDebtorId { get; set; } //Borcalan	Kontragent ID		
        public string Currency { get; set; }
        public string Guarantors { get; set; }//Zaminlər
        public string SignedBy { get; set; } //İmza edən 
        public string BackOfficeApprover { get; set; } //Back office təsdiq edən şəxs
        public DateTime BackOfficeConfirmedDate { get; set; } //Back office təsdiq edilən tarix
        public string DecisionMaker { get; set; } //Qərar qəbul edən şəxs 
        public DateTime DecisionDate { get; set; } //Qərar qəbul edilən tarix
        public string ContractNo { get; set; }  //Müqavilə No
        public string RefusalReason { get; set; }//İmtina səbəbi
        public string Expert { get; set; } // Expert  ||  long? UserId (BaseEntity) =  İstifadəçi adı
        public string CalculationType { get; set; } //Kredit hesablama tipi
        public long? SourceId { get; set; }//Mənbə 
        public Source Source { get; set; }//Mənbə 
        public long ProductId { get; set; }//Məhsul 
        public Product Product { get; set; }//Məhsul 
       // public string LoanStatus { get; set; } 
        public DateTime ContractTime { get; set; } //Müqavilə tarixi 
        public int LoanPeriod { get; set; } //Kredit müddəti
        public DateTime LoanExpireDate { get; set; } //Kreditin bitmə tarixi
        public DateTime LoanEffectiveDate { get; set; } //Kreditin qüvvəyə minmə tarixi
        public bool IsComissionFull { get; set; } //Komissiya gəliri birdəfəlik tanınır 
        public double Comission { get; set; } // Komissiya %

        [Column(TypeName = "decimal(18,4)")]
        public decimal ComissionAmount { get; set; } //Komissiya məbləği

        [Column(TypeName = "decimal(18,4)")]
        public decimal AnnuityAmount { get; set; } //ayliq odenis
        public double AnnualPercentage { get; set; } //İllik faiz dərəcəsi
        public int DiscountedMonths { get; set; } //Güzəştli aylar
        public string Status { get; set; } //Kreditin statusu	
        public string Description { get; set; }
        public LegalDebtor LegalDebtor { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }
        public string AccountNumber { get; set; }
        public double? Percentage { get; set; } // adi faiz

        [Column(TypeName = "decimal(18,4)")]
        public decimal CreditAmount { get; set; }//Kredit məbləği 
        public DateTime GivingTime { get; set; }//Kreditin Verilmə tarixi 
        public int Period { get; set; }//Müddət (ay sayı) 
        public DateTime PaymentDate { get; set; } //Odenish tarixi
     
        public double? FIFD { get; set; }

        public PaymentSchedule PaymentSchedule { get; set; } //Kredit ödəniş cədvəli

    }
}
