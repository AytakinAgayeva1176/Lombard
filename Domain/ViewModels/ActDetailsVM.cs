using Lombard.Domain.Entities;
using System.Collections.Generic;

namespace Lombard.Domain.ViewModels
{
    public class ActDetailsVM: Act
    {
        public ICollection<Act> Acts { get; set; }
    }
}
