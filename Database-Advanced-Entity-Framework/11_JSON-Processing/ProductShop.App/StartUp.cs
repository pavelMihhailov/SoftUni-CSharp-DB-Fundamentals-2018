using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProductShop.App
{
    using AutoMapper;

    using Data;
    using Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

        }

        private static void UsersAndProductsExport(ProductShopContext context)
        {
            var users = context.Users.Where(x => x.ProductsSold.Count >= 1)
                            .OrderByDescending(x => x.ProductsSold)
                            .ThenBy(x => x.LastName)
                            .Select(x => new
                            {
                                usersCount = context.Users.Count(),
                                users = new
                                {
                                    firstName = x.FirstName,
                                    lastName = x.LastName,
                                    age = x.Age,
                                    soldProducts = new
                                    {
                                        count = x.ProductsSold.Count,
                                        products = x.ProductsSold.Select(s => new
                                        {
                                            name = s.Name,
                                            price = s.Price
                                        })
                                    }
                                }
                            }).ToArray();

            var jsonObject = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/users-and-products.json", jsonObject);
        }

        private static void CategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                            .Select(x => new
                            {
                                category = x.Name,
                                productsCount = x.CategoryProducts.Count,
                                averagePrice = x.CategoryProducts.Sum(s => s.Product.Price) / x.CategoryProducts.Count,
                                totalRevenue = x.CategoryProducts.Sum(s => s.Product.Price)
                            })
                            .OrderByDescending(x => x.productsCount)
                            .ToArray();

            var jsonObject = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/categories-by-products.json", jsonObject);
        }

        private static void SuccessfullySoldProductsExport(ProductShopContext context)
        {
            var users = context.Users
                            .Where(x => x.ProductsSold.Count >= 1 && x.ProductsSold.Any(s => s.Buyer != null))
                            .OrderBy(x => x.LastName)
                            .ThenBy(x => x.FirstName)
                            .Select(x => new
                            {
                                firstName = x.FirstName,
                                lastName = x.LastName,
                                soldProducts = x.ProductsSold.Select(s => new
                                {
                                    name = s.Name,
                                    price = s.Price,
                                    buyerFirstName = s.Buyer.FirstName,
                                    buyerLastName = s.Buyer.LastName
                                })
                            }).ToArray();

            var jsonObject = JsonConvert.SerializeObject(users, Formatting.Indented);

            File.WriteAllText("../../../Json/users-sold-products.json", jsonObject);
        }

        private static void ProductsInRangeExport(ProductShopContext context)
        {
            var products = context.Products
                            .Where(x => x.Price >= 500 && x.Price <= 1000)
                            .OrderBy(x => x.Price)
                            .Select(x => new
                            {
                                name = x.Name,
                                price = x.Price,
                                seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                            }).ToArray();

            var jsonObject = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("../../../Json/products-in-range.json", jsonObject);
        }

        private static void GenerateCategories(ProductShopContext context)
        {
            var listOfCategoryProducts = new List<CategoryProduct>();
            for (int productId = 1; productId <= 200; productId++)
            {
                var categoryId = new Random().Next(1, 12);

                var categoryProduct = new CategoryProduct
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };

                listOfCategoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(listOfCategoryProducts);
            context.SaveChanges();
        }

        private static void ImportCategories(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/categories.json");

            var deserializedCategories = JsonConvert.DeserializeObject<Category[]>(jsonString);

            var listOfCategories = new List<Category>();

            foreach (var category in deserializedCategories)
            {
                if (!IsValid(category))
                {
                    continue;
                }
                listOfCategories.Add(category);
            }
            context.Categories.AddRange(listOfCategories);
            context.SaveChanges();
        }

        private static void ImportProducts(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/products.json");

            var deserializedProducts = JsonConvert.DeserializeObject<Product[]>(jsonString);

            var listOfProducts = new List<Product>();
            int count = 1;
            foreach (var product in deserializedProducts)
            {
                if (!IsValid(product))
                {
                    continue;
                }
                var buyerId = new Random().Next(1, 30);
                var sellerId = new Random().Next(31, 55);

                if (count == 4)
                {
                    count = 0;
                    product.SellerId = sellerId;
                    product.BuyerId = null;
                    listOfProducts.Add(product);
                    continue;
                }
                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                listOfProducts.Add(product);

                count++;
            }

            context.Products.AddRange(listOfProducts);
            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }

        private static void ImportUsers(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/users.json");

            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);

            var listOfUsers = new List<User>();
            foreach (var user in deserializedUsers)
            {
                if (IsValid(user))
                {
                    listOfUsers.Add(user);
                }
            }
            context.Users.AddRange(listOfUsers);
            context.SaveChanges();
        }
    }
}
