using System.ComponentModel;

namespace Lombard.Domain.Enums
{
    public enum LoanAssignment
    {
        [Description("Dövriyyə vəsaitinin artırılması")]
        TurnoverAmountIncreasing,
        [Description("Əsas vəsaitin artırılması")]
        MainAmountIncreasing,
        [Description("Şəxsi istehlak")]
        PrivateConsumption
    }
}
