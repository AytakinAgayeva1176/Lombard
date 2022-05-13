using System;

namespace Lombard.Domain.ViewModels
{
    public class FamilyMemberVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        public double Salary { get; set; }
        public string MobileNumber { get; set; }
        public string MobileAdditional { get; set; }
        public string Email { get; set; }
        public long RelationTypeId { get; set; }
        public long BorcAlanFizikiId { get; set; }
    }
}
