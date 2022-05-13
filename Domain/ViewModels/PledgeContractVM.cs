using Lombard.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Lombard.Domain.ViewModels
{
    public class PledgeContractVM
    {
        public int Id { get; set; }
        public List<Pledge> PledgeList { get; set; } = new List<Pledge>();
        public List<long> PledgeIdList { get; set; } = new List<long>();
        public long? ParentId { get; set; }
        public long CorporativeContractId { get; set; }
        public string ContractNumber { get; set; }
        public DateTime Date { get; set; }//Kreditin Verilmə tarixi 
        public double TotalGoldWeight { get; set; } //"Ümumi cəkisi"
        public double TotalGoldNetWeight { get; set; }//"Xalis çəkisi"
        public double TotalGoldLikvidPrice { get; set; }
        public double TotalGoldMarketPrice { get; set; }
      
    }
}
