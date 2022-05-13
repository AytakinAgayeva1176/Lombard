using Lombard.Domain.Entities;
using Lombard.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Lombard.Domain.ViewModels
{
    public class ActVM
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public ActTypes ActType { get; set; }
        public long? IndividualDebtorId { get; set; }
        public long? LegalDebtorId { get; set; }
        public decimal TotalGoldWeight { get; set; } 
        public decimal TotalJewelsWeight { get; set; } 
        public decimal TotalGoldNetWeight { get; set; }
        public decimal TotalGoldLikvidPrice { get; set; }
        public decimal TotalGoldMarketPrice { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<Gold> Golds { get; set; } = new List<Gold>();

    }
}
