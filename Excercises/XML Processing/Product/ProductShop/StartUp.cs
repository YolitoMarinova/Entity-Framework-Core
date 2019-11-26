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

                string result = GetUsersWithProducts(db);

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
                    .OrderBy(p => p.Price)
                    .Take(10)
                    .ProjectTo<ProductInRangeDTO>()
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
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ProjectTo<UserProductsDTO>()
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(UserProductsDTO[]), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var usersWriter = new StringWriter(result))
            {
                xmlSerializer.Serialize(usersWriter, usersWithSoldProducts, namespaces);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var categories = context
                .Categories
                .ProjectTo<CategoryByProductDTO>()
                .OrderByDescending(c => c.ProductsCount)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CategoryByProductDTO[]), new XmlRootAttribute("Categories"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var categoriesWriter = new StringWriter(result))
            {
                xmlSerializer.Serialize(categoriesWriter, categories, namespaces);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var users = context
                .Users
                .Where(us => us.ProductsSold.Any())
                .Select(us => new UserSoldProductsDTO()
                {
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    Age = us.Age,
                    SoldProducts = new SoldProductsDTO()
                    {
                        Count = us.ProductsSold.Count(),
                        Products = us.ProductsSold.Select(p => new SoldProductDTO()
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(us => us.SoldProducts.Count)
                .Take(10)
                .ToArray();

            var usersOutput = new UserOutputDTO()
            {
                UsersCount = users.Length,
                Users = users
            };

            var xmlSerializer = new XmlSerializer(typeof(UserOutputDTO), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var usersWriter = new StringWriter(result))
            {
                xmlSerializer.Serialize(usersWriter, usersOutput, namespaces);
            }

            return result.ToString().TrimEnd();
        }
    }
}