using Lombard.Domain.Enums;
using System.Collections.Generic;

namespace Lombard.Domain.Entities
{
    // Borcalan ümumi field-lər
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string IsResident { get; set; }
        public string RegistrationStatus { get; set; }
        public string City { get; set; }
        public string CurrentLivingAddress { get; set; }
        public string RegisteredAddress { get; set; }
        public string MobileNumber { get; set; }
        public string MobileAdditional1 { get; set; }
        public string MobileAdditional2 { get; set; }
        public string MobileAdditional3 { get; set; }
        public string MobileAdditional4 { get; set; }
        public string Phone { get; set; }
        public string PhoneAdditional { get; set; }
        public string Email { get; set; }
        public string VOEN { get; set; }
        public string Description { get; set; }
    }
}
