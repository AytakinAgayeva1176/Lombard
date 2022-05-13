using System.ComponentModel;

namespace Lombard.Domain.Enums
{

    public enum RecordStatus
    {
        [Description("IsActive")]
        IsActive,
        [Description("InActive")]
        InActive,
        [Description("Deleted")]
        Deleted

    }
}
