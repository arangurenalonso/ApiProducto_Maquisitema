using Domain.Common;
using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product: BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string? Name { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;

        public int? Stock { get; set; } 

        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Description { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? Price { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
