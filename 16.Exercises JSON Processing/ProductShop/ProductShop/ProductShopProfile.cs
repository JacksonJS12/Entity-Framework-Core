using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop;

using AutoMapper;
public class ProductShopProfile : Profile
{
    public ProductShopProfile()
    {
        //User
        this.CreateMap<ImportUserDto, User>();
    }
}
