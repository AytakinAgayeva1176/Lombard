using System.ComponentModel;

namespace Lombard.Domain.Enums
{
    public enum ContractStatuses
    {
        [Description("Yeni əlavə edilmiş")]
        NewAdded,
        [Description("Təsdiq edilib")]
        Active,
        [Description("İmtina olunmuş")]
        Refused,
        [Description("Bitmiş")]
        Expired,
        [Description("Təsdiq gözləyir")]
        Pending,
        [Description("Silinmiş")]
        Deleted,
    }
}
