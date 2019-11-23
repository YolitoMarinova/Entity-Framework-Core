namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarDealer.Data;
    using CarDealer.DTO;
    using CarDealer.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new CarDealerContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                string inputJson = File.ReadAllText("../../../Datasets/sales.json");

                string result = GetSalesWithAppliedDiscount(db);

                Console.WriteLine(result);
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.AddRange(suppliers);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert
                .DeserializeObject<Part[]>(inputJson)
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId));

            context.AddRange(parts);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDTO = JsonConvert.DeserializeObject<ImportCarDTO[]>(inputJson);

            List<Car> cars = new List<Car>();
            List<PartCar> partsCars = new List<PartCar>();

            foreach (var carDTO in carsDTO)
            {
                Car car = new Car()
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TravelledDistance
                };

                cars.Add(car);

                foreach (var partId in carDTO.PartsId.Distinct())
                {
                    PartCar partCar = new PartCar()
                    {
                        PartId = partId,
                        Car = car
                    };

                    partsCars.Add(partCar);
                }
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partsCars);

            context.SaveChanges();

            return $"Successfully imported {carsDTO.Length}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.AddRange(sales);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context
                    .Customers
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .Select(c => new ExportCustomerDTO
                    {
                        Name = c.Name,
                        BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        IsYoungDriver = c.IsYoungDriver
                    })
                    .ToList();

            string jsonResult = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return jsonResult;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context
                    .Cars
                    .Where(c => c.Make == "Toyota")
                    .Select(c => new CarsByMakeDTO
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    })
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .ToList();

            var jsonResult = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);

            return jsonResult;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSppliers = context
                    .Suppliers
                    .Where(s => s.IsImporter == false)
                    .Select(s => new LocalSuppliersDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        PartsCount = s.Parts.Count
                    })
                    .ToList();

            var jsonResult = JsonConvert.SerializeObject(localSppliers, Formatting.Indented);

            return jsonResult;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsAndParts = context
                    .Cars
                    .Select(c => new CarPartsDTO
                    {
                        car = new CarDTO
                        {
                            Make = c.Make,
                            Model = c.Model,
                            TravelledDistance = c.TravelledDistance
                        },
                        parts = c.PartCars
                        .Select(p => new PartDTO
                        {
                            Name = p.Part.Name,
                            Price = $"{p.Part.Price:F2}"
                        })
                        .ToList()
                    })
                    .ToList();

            var jsonResult = JsonConvert.SerializeObject(carsAndParts, Formatting.Indented);

            return jsonResult;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                    .Customers
                    .Where(c => c.Sales.Count > 0)
                    .Select(c => new CustomerDTO
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count,
                        SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                    })
                    .OrderByDescending(c => c.SpentMoney)
                    .ThenByDescending(c => c.BoughtCars)
                    .ToList();

            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonResult = JsonConvert.SerializeObject(customers, jsonSettings);

            return jsonResult;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                    .Sales
                    .Take(10)
                    .Select(s => new SaleDTO
                    {
                        car = new CarDTO
                        {
                            Make = s.Car.Make,
                            Model = s.Car.Model,
                            TravelledDistance = s.Car.TravelledDistance
                        },
                        customerName = s.Customer.Name,
                        Discount = $"{s.Discount:F2}",
                        price = $"{s.Car.PartCars.Sum(p => p.Part.Price):F2}",
                        priceWithDiscount = $"{(s.Car.PartCars.Sum(p => p.Part.Price)- (s.Car.PartCars.Sum(p => p.Part.Price)*s.Discount)/100):F2}"
                    })
                    .ToList();

            var jsonResult = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return jsonResult;
        }
    }
}