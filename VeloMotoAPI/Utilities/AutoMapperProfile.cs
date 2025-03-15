using AutoMapper;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Products, ProductsDTO>().ReverseMap();
            CreateMap<Categories, CategoriesDTO>().ReverseMap();
            CreateMap<Manufacturers, ManufacturersDTO>().ReverseMap();
            CreateMap<Orders, OrdersDTO>().ReverseMap();
            CreateMap<Sales, SalesDTO>().ReverseMap();
            CreateMap<SalesInvoice, SalesInvoiceDTO>().ReverseMap();
            CreateMap<Prices, PricesDTO>().ReverseMap();
            CreateMap<Images, ImagesDTO>().ReverseMap();
            CreateMap<Providers, ProvidersDTO>().ReverseMap();
            CreateMap<Purchases, PurchasesDTO>().ReverseMap();
        }
    }
} 