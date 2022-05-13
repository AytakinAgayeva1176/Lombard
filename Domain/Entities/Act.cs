using Lombard.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.Domain.Entities
{
    //Individual müqavilə aktı
    public class Act:BaseEntity
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public Act Parent { get; set; }
        public ActTypes ActType { get; set; }
        public long? IndividualDebtorId { get; set; }
        public long? LegalDebtorId { get; set; }
        public double TotalGoldWeight { get; set; } //"Ümumi cəkisi"
        public double TotalJewelsWeight { get; set; } //"Daşın çəkisi"
        public double TotalGoldNetWeight { get; set; }//"Xalis çəkisi"
        public double TotalGoldLikvidPrice { get; set; }
        public double TotalGoldMarketPrice { get; set; }
        public double TotalGoldCount { get; set; } //"Ümumi say"
        public string FileName { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }


        public LegalDebtor LegalDebtor { get; set; }
        public IndividualDebtor IndividualDebtor { get; set; }
        public ICollection<Gold> Golds { get; set; } = new List<Gold>();

        public ICollection<ActIndividualContract> ActIndividualContracts { get; set; }


    }
}
