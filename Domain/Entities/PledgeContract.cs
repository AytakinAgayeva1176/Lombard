using Lombard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{

    // Girov müqaviləsinə əlavə və çıxarış
    // Əsas girov müqaviləsi sistematik olaraq yaradılmır. Çünki field-lər əsas müqavilə ilə eynidir. 
    public class PledgeContract : BaseEntity
    {
       // public long? ParentId { get; set; }
        public ActTypes ActType { get; set; }
        public long CorporativeContractId { get; set; }
        public string ContractNumber { get; set; }
        public DateTime Date { get; set; }   //Kreditin Verilmə tarixi 
        public double TotalGoldWeight { get; set; }  //"Ümumi cəkisi"
        public double TotalGoldNetWeight { get; set; }  //"Xalis çəkisi"
        public double TotalGoldLikvidPrice { get; set; } //"Likvid qiymət"
        public double TotalGoldMarketPrice { get; set; }//"Bazar qiyməti"

        //public PledgeContract Parent { get; set; }
        public CorporativeContract CorporativeContract { get; set; }
        public ICollection<Pledge> PledgeList { get; set; } = new List<Pledge>(); //"Girov listi"
    }
}
