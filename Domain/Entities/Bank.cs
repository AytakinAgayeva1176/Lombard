using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lombard.Domain.Entities
{
    public class Bank : BaseEntity
    {
        public string Name { get; set; }
        public string Branch { get; set; }
        public string ForPrint { get; set; }
        public string VOEN { get; set; }
        public string CorrespondentAccount { get; set; }
        public string SWIFT { get; set; }
        [Required]
        public string IBAN { get; set; }
        [Required]
        public string Code { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string OtherDetails { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

    }
}
