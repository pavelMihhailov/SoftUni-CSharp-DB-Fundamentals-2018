using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using ProductShop.Data;
using ProductShop.Dto;
using ProductShop.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new ProductShopDbContext();

            var products = db.Products.Where(x => x.Price >= 1000 && x.Price <= 2000 && x.Buyer != null)
                .OrderByDescending(x => x.Price)
                .Select(x => new ProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                }).ToArray();
            var sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(sb), products, xmlNamespaces);

            File.WriteAllText("../../../Xml/products-in-range.xml", sb.ToString());
        }

        private static void ReadCategoryProducts()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();

            var categoryProducts = new List<CategoryProduct>();

            for (int productId = 1; productId < 200; productId++)
            {
                var categoryId = new Random().Next(1, 11);

                var categoryProduct = new CategoryProduct()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };
                categoryProducts.Add(categoryProduct);
            }


            var context = new ProductShopDbContext();
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
