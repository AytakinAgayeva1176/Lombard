using Lombard.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Lombard.Domain.Entities
{
    //  İndividual müqavilə
    public class IndividualContract : Contract
    {
        public ICollection<ActIndividualContract> ActIndividualContracts { get; set; }
        public ICollection<GuaranterContract> GuaranterIndividualContract { get; set; }
        
    }
}
