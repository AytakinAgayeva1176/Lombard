using Lombard.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Lombard.Domain.Entities
{
    // Fiziki borcalan
    public class IndividualDebtor : Person
    {
        public bool IsOwner { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string IDFincode { get; set; }
        public string IDSerialNumber { get; set; }
        public string IDOrganizationName { get; set; }
        public string Citizenship { get; set; }
        public DateTime IDExpireDate { get; set; }
        public DateTime? IDGiveDate { get; set; }
        public string MarialStatuses { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public ICollection<Act> Acts { get; set; } = new List<Act>();
        public ICollection<CorporativeContract> CorporativeContracts { get; set; } = new List<CorporativeContract>();
        public ICollection<IndividualContract> IndividualContracts { get; set; } = new List<IndividualContract>();
        public ICollection<GuaranterContract> GuaranterIndividualContracts { get; set; }=new List<GuaranterContract>();
    }
}
