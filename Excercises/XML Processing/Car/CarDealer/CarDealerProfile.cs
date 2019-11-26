using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierImportDTO, Supplier>();

            this.CreateMap<PartImportDTO, Part>();

            this.CreateMap<CarImportDTO, Car>();

            this.CreateMap<CustomerImportDTO, Customer>();

            this.CreateMap<SaleImportDTO, Sale>();

            this.CreateMap<Car, CarWithDistanceDTO>();

            this.CreateMap<Car, CarFromBMWDTO>();

            this.CreateMap<Supplier, LocalSuppliersDTO>();
        }
    }
}
