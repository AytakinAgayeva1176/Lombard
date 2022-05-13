using Lombard.Domain.Enums;
using System;

namespace Lombard.Domain.ViewModels
{
    public class IndividualDebtorVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string IDFincode { get; set; }
        public string IDSerialNumber { get; set; }
        public string IDOrganizationName { get; set; }
        public string Citizenship { get; set; }
        public string CurrentLivingAddress { get; set; }
        public string RegisteredAddress { get; set; }
        public string MobileNumber { get; set; }
        public string MobileAdditional1 { get; set; }
        public string MobileAdditional2 { get; set; }
        public string Phone { get; set; }
        public string PhoneAdditional { get; set; }
        public string Email { get; set; }
        public DateTime IDExpireDate { get; set; }
        public DateTime IDGiveDate { get; set; }
        public MarialStatuses MarialStatuses { get; set; }
        public GenderTypes Gender { get; set; }
    }
}
