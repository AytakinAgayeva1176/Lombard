using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.ViewModels
{
    public class IndividualContractVM
    {
        public int Id { get; set; }
        public IndividualContract IndividualContract { get; set; }
        public List<long> Acts { get; set; }=new List<long>();
        public string DebtorId { get; set; }
        public string GuaranterId { get; set; }
    }
}
