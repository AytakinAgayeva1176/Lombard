using System;

namespace Lombard.Domain.Entities
{
    public class Bail:BaseEntity
    {

        public long? CorporativeContractID { get; set; }//Kredit ID	Müqavilə nömrəsi
        public long? IndividualContractID { get; set; }//Kredit ID	Müqavilə nömrəsi
        public long? IndividualDebtorId { get; set; } //Borcalan	Kontragent ID		
        public long? LegalDebtorId { get; set; } //Borcalan	Kontragent ID		
        public string Type { get; set; } //Girov növü Daşınmaz əmlak/ Daşınan əmlak/ Hesabda olan nəğd vəsait/ Avtonəqliyyat vasitələri/ Məişət əşyaları/ Avadanlıq/ Qızıl və zinyət əşyaları
        public string Description { get; set; } //Girov predmetinin təsviri Nvarchar, 250
        public DateTime EvaluationDate { get; set; }//Qiymətləndirmə tarixi   date
        public string Evaluator { get; set; } //Qiymətləndirici Nvarchar, 250
        public DateTime RegistrationDate { get; set; }//Qeydiyyat tarixi    date
        public LegalDebtor LegalDebtor { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }
        public CorporativeContract CorporativeContracts { get; set; }
        public IndividualContract IndividualContracts { get; set; }

    }
}
