using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{
    // Qızıl Əyarı
    public class Hallmark:BaseEntity
    {
        public string TypeName { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal LikvidPriceOfOneGram { get; set; } //"1 qr likvid qiyməti"
        [Column(TypeName = "decimal(18,4)")]
        public decimal MarketPriceOfOneGram { get; set; }//1 qr bazar qiyməti
    }
}
