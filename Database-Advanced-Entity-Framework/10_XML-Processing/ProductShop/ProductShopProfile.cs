using AutoMapper;
using ProductShop.Dto;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //CreateMap<UserDto, User>();
            CreateMap<ProductDto, Product>();
            //CreateMap<CategoryDto, Category>();
        }
    }
}
