using System.ComponentModel;

namespace Lombard.Domain.Enums
{
    public enum ResidentStatus
    {
        [Description("Rezident")]
        Resident,
        [Description("Qeyri-Rezident")]
        NonResident
    }
}
