namespace Application.Features.Products.Vms
{
    using Application.Features.Products.Dto;
    using Application.Models;
    using Domain.Enum;
    public class ProductByIdDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public Decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }
        public Decimal DiscontPercent { get; set; }
        public Discont? Discont { get; set; }
        public Decimal FinalPrice { get; set; }
    }
}
