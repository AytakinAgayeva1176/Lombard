using System;

namespace Lombard.Domain.ViewModels
{
    public class BankAccountVM
    {
        public string Code { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int BankId { get; set; }
        public bool IsMainAccount { get; set; }
    }
}
