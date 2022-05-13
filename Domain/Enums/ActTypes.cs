using System.ComponentModel;

namespace Lombard.Domain.Enums
{
    public enum ActTypes
    {
        [Description("Əsas")]
        Main,
        [Description("Əlavə")]
        Addition,
        [Description("Çıxarış")]
        Extractions,
        [Description("Yekun")]
        Final
    }
}
