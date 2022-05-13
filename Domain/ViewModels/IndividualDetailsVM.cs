using Lombard.Domain.Entities;
using System.Collections.Generic;

namespace Lombard.Domain.ViewModels
{
    public class IndividualDetailsVM
    {
        public IndividualContract IndividualContract { get; set; }
        public List<Act> Acts { get; set; } = new List<Act>();
    }
}
