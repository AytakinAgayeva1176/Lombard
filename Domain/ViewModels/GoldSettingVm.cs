using Lombard.Domain.Entities;
using System.Collections.Generic;

namespace Lombard.Domain.ViewModels
{
    public class GoldSettingVm
    {
        public IEnumerable<GoldType> GoldTypes { get; set; }
        public IEnumerable<Hallmark> Hallmarks { get; set; }
    }
}
