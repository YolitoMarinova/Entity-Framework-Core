using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierImportDTO, Supplier>();

            this.CreateMap<PartImportDTO, Part>();

            this.CreateMap<CarImportDTO, Car>();

            this.CreateMap<CarImportDTO, PartCar>()
                .ForMember(cfg => cfg.PartId, src => src.MapFrom(cfg => cfg.PartsId))
                .ForMember(cfg => cfg.Car, src => src.MapFrom(cfg => cfg));

            this.CreateMap<CustomerImportDTO, Customer>();

            this.CreateMap<SaleImportDTO, Sale>();

            this.CreateMap<Car, CarWithDistanceDTO>();

            this.CreateMap<Car, CarFromBMWDTO>();

            this.CreateMap<Supplier, LocalSuppliersDTO>();

            this.CreateMap<Part, PartsExportDTO>();

            this.CreateMap<Car, CarsWIthListOfPartsDTO>()
                .ForMember(cfg => cfg.Parts, src => src.MapFrom(cfg => cfg.PartCars.Select(p => p.Part).OrderByDescending(p => p.Price)));

            this.CreateMap<Customer, CustomerBoughtsDTO>()
                .ForMember(cfg => cfg.FullName, src => src.MapFrom(cfg => cfg.Name))
                .ForMember(cfg => cfg.BoughtCars, src => src.MapFrom(cfg => cfg.Sales.Count))
                .ForMember(cfg => cfg.TotalSpentMoney, src => src.MapFrom(cfg => cfg.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))));

            this.CreateMap<Car, CarSaleExportDTO>();

            this.CreateMap<Sale, SaleExportDTO>()
                .ForMember(cfg => cfg.Car, src => src.MapFrom(cfg => cfg.Car))
                .ForMember(cfg => cfg.CustomerName, src => src.MapFrom(cfg => cfg.Customer.Name))
                .ForMember(cfg => cfg.Price, src => src.MapFrom(cfg => cfg.Car.PartCars.Sum(p => p.Part.Price)))
                .ForMember(cfg => cfg.PriceWithDiscount, src => src.MapFrom(cfg => cfg.Car.PartCars.Sum(p => p.Part.Price) - ((cfg.Car.PartCars.Sum(p => p.Part.Price) * cfg.Discount) / 100)));
        }
    }
}
