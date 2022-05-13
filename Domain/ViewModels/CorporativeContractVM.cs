using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.ViewModels
{
    public class CorporativeContractVM
    {
        public int Id { get; set; }
        public CorporativeContract CorporativeContract { get; set; }
        public string DebtorId { get; set; }
        public string GuaranterId { get; set; }
    }
}
