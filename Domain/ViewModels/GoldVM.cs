namespace Lombard.Domain.ViewModels
{
    public class GoldVM
    {
        public long GoldTypeId { get; set; }
        public long HallmarkId { get; set; }
        public double OneGramLikvidPrice { get; set; } //"1 qr likvid qiyməti"
        public double OneGramStorePrice { get; set; }//1 qr bazar qiyməti

        public int ItemsCount { get; set; }//"Əşya-ların sayı"
        public double TotalWeight { get; set; } //"Ümumi cəkisi"
        public double JewelWeight { get; set; } //"Daşın çəkisi"
        public double NetWeight { get; set; }//"Xalis çəkisi"
        public double LikvidPrice { get; set; }
        public double MarketPrice { get; set; }
        public string Description { get; set; }
    }
}
