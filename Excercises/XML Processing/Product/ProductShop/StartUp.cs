namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var db = new ProductShopContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //var inputXml = File.ReadAllText("../../../Datasets/categories-products.xml");

                string result = GetSoldProducts(db);

                Console.WriteLine(result);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportUserDTO[]), new XmlRootAttribute("Users"));

            ImportUserDTO[] deserializedUsers;

            using (var usersReader = new StringReader(inputXml))
            {
                deserializedUsers = (ImportUserDTO[])xmlSerializer.Deserialize(usersReader);
            }

            User[] users = Mapper.Map<User[]>(deserializedUsers);

            context.AddRange(users);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProductDTO[]), new XmlRootAttribute("Products"));

            ImportProductDTO[] desirializeProducts;

            using (var productReader = new StringReader(inputXml))
            {
                desirializeProducts = (ImportProductDTO[])xmlSerializer.Deserialize(productReader);
            }

            Product[] products = Mapper.Map<Product[]>(desirializeProducts);

            context.AddRange(products);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryDTO[]), new XmlRootAttribute("Categories"));

            ImportCategoryDTO[] deserializerCategories;

            using (var categoriesReader = new StringReader(inputXml))
            {
                deserializerCategories = (ImportCategoryDTO[])xmlSerializer.Deserialize(categoriesReader);
            }

            Category[] categories = Mapper
                .Map<Category[]>(deserializerCategories)
                .Where(c => c.Name != null)
                .ToArray();

            context.AddRange(categories);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductDTO[]), new XmlRootAttribute("CategoryProducts"));

            ImportCategoryProductDTO[] deserizlizeCateogriesProducts;

            using (var categoryProductReader = new StringReader(inputXml))
            {
                deserizlizeCateogriesProducts = (ImportCategoryProductDTO[])xmlSerializer.Deserialize(categoryProductReader);
            }

            var categoriesProducts = Mapper.Map<CategoryProduct[]>(deserizlizeCateogriesProducts);

            context.AddRange(categoriesProducts);

            int addedCount = context.SaveChanges();

            return $"Successfully imported {addedCount}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var productsInRange = context
                    .Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .Select(p => new ProductInRangeDTO
                    {
                        Name = p.Name,
                        Price = p.Price,
                        BuyerFullName = p.Buyer.FirstName + " " + p.Buyer.LastName
                    })
                    .OrderBy(p => p.Price)
                    .Take(10)
                    .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ProductInRangeDTO[]), new XmlRootAttribute("Products"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var productWriter = new StringWriter(result))
            {
                xmlSerializer.Serialize(productWriter, productsInRange, namespaces);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var usersWithSoldProducts = context
                .Users
                .Where(u => u.ProductsSold.Count > 0)
                .Select(u => new UserWithSoldProductsDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold                            
                            .Select(p => new SoldProductDTO
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(UserWithSoldProductsDTO[]),new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var usersWriter = new StringWriter(result))
            {
                xmlSerializer.Serialize(usersWriter, usersWithSoldProducts, namespaces);
            }

            return result.ToString().TrimEnd();
        }
    }
}