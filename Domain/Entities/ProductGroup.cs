using System.Collections.Generic;

namespace Lombard.Domain.Entities
{
    public class ProductGroup :BaseEntity
    {
        public string Name { get; set; }
        public bool IsFiveWorkdaysSchedule { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
