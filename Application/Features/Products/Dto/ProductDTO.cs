namespace Application.Features.Products.Vms
{
    using Application.Features.Products.Dto;
    using Domain.Enum;
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public CategoryDTO? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
