namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Models;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using System.Linq;
    using System.Collections.Generic;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDTO, User>();

            this.CreateMap<ImportProductDTO, Product>();

            this.CreateMap<ImportCategoryDTO, Category>();

            this.CreateMap<ImportCategoryProductDTO, CategoryProduct>();

            this.CreateMap<Product, ProductInRangeDTO>()
                .ForMember(cfg => cfg.BuyerFullName, src => src.MapFrom(cfg => cfg.Buyer.FirstName + " " + cfg.Buyer.LastName));

            this.CreateMap<Product, SoldProductDTO>();

            this.CreateMap<User, UserProductsDTO>()
                .ForMember(cfg => cfg.SoldProducts, src => src.MapFrom(cfg => cfg.ProductsSold));

            this.CreateMap<Category, CategoryByProductDTO>()
                .ForMember(cfg => cfg.ProductsCount, src => src.MapFrom(cfg => cfg.CategoryProducts.Count))
                .ForMember(cfg => cfg.AverageProductPrice, src => src.MapFrom(cfg => cfg.CategoryProducts.Average(p => p.Product.Price)))
                .ForMember(cfg => cfg.TotalRevenue, src => src.MapFrom(cfg => cfg.CategoryProducts.Sum(p => p.Product.Price)));
        }
    }
}
