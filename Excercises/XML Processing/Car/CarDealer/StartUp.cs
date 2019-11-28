using CarDealer.Data;
namespace CarDealer
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(x => x.AddProfile<CarDealerProfile>());

            using (var db = new CarDealerContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //var inputXml = File.ReadAllText("../../../Datasets/sales.xml");

                var result = GetSalesWithAppliedDiscount(db);

                Console.WriteLine(result);
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SupplierImportDTO[]), new XmlRootAttribute("Suppliers"));

            SupplierImportDTO[] deserializedSuppliers;

            using (var suppliersReader = new StringReader(inputXml))
            {
                deserializedSuppliers = (SupplierImportDTO[])xmlSerializer.Deserialize(suppliersReader);
            }

            Supplier[] suppliers = Mapper.Map<Supplier[]>(deserializedSuppliers);

            context.AddRange(suppliers);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartImportDTO[]), new XmlRootAttribute("Parts"));

            PartImportDTO[] deserializeParts;

            using (var partsReader = new StringReader(inputXml))
            {
                deserializeParts = (PartImportDTO[])xmlSerializer.Deserialize(partsReader);
            }

            Part[] parts = Mapper
                .Map<Part[]>(deserializeParts)
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .ToArray();

            context.AddRange(parts);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CarImportDTO[]), new XmlRootAttribute("Cars"));

            var carsDto = (CarImportDTO[])xmlSerializer.Deserialize(new StringReader(inputXml));

            List<Car> cars = new List<Car>();
            List<PartCar> partsCars = new List<PartCar>();

            foreach (var carDto in carsDto)
            {
                var car = Mapper.Map<Car>(carDto);

                var parts = carDto
                    .PartsId
                    .Select(pdto => pdto.PartId)
                    .Where(pdto => context.Parts.Any(p => p.Id == pdto))
                    .Distinct()
                    .ToArray();

                foreach (var partId in parts)
                {
                    var partCar = new PartCar
                    {
                        Car = car,
                        PartId = partId
                    };

                    partsCars.Add(partCar);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partsCars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerImportDTO[]), new XmlRootAttribute("Customers"));

            CustomerImportDTO[] deserializeCustomers;

            using (var customersReader = new StringReader(inputXml))
            {
                deserializeCustomers = (CustomerImportDTO[])xmlSerializer.Deserialize(customersReader);
            }

            Customer[] parts = Mapper.Map<Customer[]>(deserializeCustomers);

            context.AddRange(parts);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaleImportDTO[]), new XmlRootAttribute("Sales"));

            SaleImportDTO[] deserializeSales;

            using (var salesReader = new StringReader(inputXml))
            {
                deserializeSales = (SaleImportDTO[])xmlSerializer.Deserialize(salesReader);
            }

            Sale[] sales = Mapper.Map<Sale[]>(deserializeSales)
                .Where(s => context.Cars.Any(c => c.Id == s.CarId))
                .ToArray();

            context.AddRange(sales);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context
                    .Cars
                    .Where(c => c.TravelledDistance > 2000000)
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .ProjectTo<CarWithDistanceDTO>()
                    .Take(10)
                    .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CarWithDistanceDTO[]), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            StringBuilder result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), cars, namespaces);

            return result.ToString().TrimEnd();
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context
                        .Cars
                        .Where(c => c.Make == "BMW")
                        .ProjectTo<CarFromBMWDTO>()
                        .OrderBy(c => c.Model)
                        .ThenByDescending(c => c.TravelledDistance)
                        .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CarFromBMWDTO[]), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            StringBuilder result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), cars, namespaces);

            return result.ToString().TrimEnd();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suplpiers = context
                            .Suppliers
                            .Where(s => !s.IsImporter)
                            .ProjectTo<LocalSuppliersDTO>()
                            .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LocalSuppliersDTO[]), new XmlRootAttribute("suppliers"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            StringBuilder result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), suplpiers, namespaces);

            return result.ToString().TrimEnd();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                           .Cars
                           .OrderByDescending(c => c.TravelledDistance)
                           .ThenBy(c => c.Model)
                           .Take(5)
                           .ProjectTo<CarsWIthListOfPartsDTO>()
                           .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CarsWIthListOfPartsDTO[]), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            StringBuilder result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), cars, namespaces);

            return result.ToString().TrimEnd();
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                               .Customers
                               .Where(c => c.Sales.Count > 0)
                               .ProjectTo<CustomerBoughtsDTO>()
                               .OrderByDescending(c => c.TotalSpentMoney)
                               .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerBoughtsDTO[]), new XmlRootAttribute("customers"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            StringBuilder result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), customers, namespaces);

            return result.ToString().TrimEnd();
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                          .Sales
                          .ProjectTo<SaleExportDTO>()
                          .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaleExportDTO[]), new XmlRootAttribute("sales"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            StringBuilder result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), sales, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}