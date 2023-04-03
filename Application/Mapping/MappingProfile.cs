using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Dto;
using Application.Features.Products.Vms;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductByIdDTO>();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            CreateMap<Category, CategoryDTO>();
        }
    }
}
