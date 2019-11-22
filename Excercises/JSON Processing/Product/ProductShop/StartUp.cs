using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            using (var db = new ProductShopContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();


                string inputJson = File.ReadAllText("../../../Datasets/categories-products.json");

                //ImportUsers(db, inputJson);
                //ImportProducts(db,inputJson);
                //ImportCategories(db, inputJson);
                //ImportCategoryProducts(db, inputJson);
                //GetProductsInRange(db);

                string json = GetUsersWithProducts(db);

                Console.WriteLine(json);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.AddRange(users);

            int addedUsersCount = context.SaveChanges();

            return $"Successfully imported {addedUsersCount}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.AddRange(products);

            int addedProductsCount = context.SaveChanges();

            return $"Successfully imported {addedProductsCount}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert
                .DeserializeObject<Category[]>(inputJson)
                .Where(n => n.Name != null)
                .ToArray();

            context.AddRange(categories);

            int addedCategoriesCount = context.SaveChanges();

            return $"Successfully imported {addedCategoriesCount}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.AddRange(categoriesProducts);

            int countOfAdded = context.SaveChanges();

            return $"Successfully imported {countOfAdded}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                    .Products
                    .Where(p => p.Price >= 500m && p.Price <= 1000m)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        seller = p.Seller.FirstName + " " + p.Seller.LastName
                    })
                    .OrderBy(p => p.price);

            var result = JsonConvert.SerializeObject(products, Formatting.Indented);

            return result;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var usersWithBuiers = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                    .Where(p => p.BuyerId != null)
                                    .Select(p => new
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName,
                                        buyerLastName = p.Buyer.LastName
                                    })
                })
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName);

            string jsonResult = JsonConvert.SerializeObject(usersWithBuiers, Formatting.Indented);

            return jsonResult;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                    .Categories
                    .Select(c => new
                    {
                        category = c.Name,
                        productsCount = c.CategoryProducts.Count,
                        averagePrice = $"{c.CategoryProducts.Average(p => p.Product.Price):F2}",
                        totalRevenue = $"{c.CategoryProducts.Sum(p => p.Product.Price):F2}",
                    })
                    .OrderByDescending(c => c.productsCount)
                    .ToList();

            var jsonResult = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return jsonResult;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                    .Users
                    .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                    .OrderByDescending(u => u.ProductsSold.Count(p => p.Buyer != null))
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count(p => p.Buyer !=null),
                            products = u.ProductsSold
                            .Where(p => p.Buyer !=null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                            .ToList()
                        }
                    })
                    .ToList();

            var userOutput = new
            {
                usersCount = users.Count,
                users = users
            };

            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonResult = JsonConvert.SerializeObject(userOutput, jsonSettings);

            return jsonResult;
        }
    }
}