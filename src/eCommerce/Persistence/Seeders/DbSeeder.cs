using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders;
internal static class DbSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        Random rnd = Random.Shared;

        List<Guid> categoryIds = Enumerable.Range(1, 10).Select(_ => Guid.NewGuid()).ToList();
        List<Guid> productIds = Enumerable.Range(1, 15).Select(_ => Guid.NewGuid()).ToList();

        modelBuilder.Entity<Category>().HasData(
            new List<Category>{
                    new() { Id = categoryIds[0], Name = "Fresh Meat", Description = "Fresh Meat", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[1], Name = "Vegetables", Description = "Vegetables", CreatedDate = DateTime.UtcNow},
                    new() { Id = categoryIds[2], Name = "Fresh Fruits", Description = "Fresh Fruits", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[3], Name = "Dried Fruits & Nuts", Description = "Dried Fruits & Nuts", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[4], Name = "Ocean Foods", Description = "Ocean Foods", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[5], Name = "Butter & Eggs", Description = "Butter & Eggs", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[6], Name = "Fastfood", Description = "Fastfood", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[7], Name = "Oatmeal", Description = "Oatmeal", CreatedDate = DateTime.UtcNow },
                    new() { Id = categoryIds[8], Name = "Juices", Description = "Juices", CreatedDate = DateTime.UtcNow }
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new List<Product>{
                    new() {
                        Id = productIds[0],
                        Name = "Mixed Fruit Juice",
                        CategoryId = categoryIds[8],
                        Price = rnd.Next(10, 540),
                        Description = "Mixed Fruit Juice",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,

                    },
                    new() {
                        Id = productIds[1],
                        Name = "Mango",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Mango",
                        QuantityAvailable = 50,
                        CreatedDate = DateTime.UtcNow,

                    },
                    new() {
                        Id = productIds[2],
                        Name = "Hamburger",
                        CategoryId = categoryIds[7],
                        Price = rnd.Next(10, 540),
                        Description = "Hamburger",
                        QuantityAvailable = 20,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[3],
                        Name = "Red Meat",
                        CategoryId = categoryIds[1],
                        Price = rnd.Next(10, 540),
                        Description = "Meat",
                        QuantityAvailable = 50,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[4],
                        Name = "Banana",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Banana",
                        QuantityAvailable = 75,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[5],
                        Name = "Fig",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Fig",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[6],
                        Name = "Apple",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Apple",
                        QuantityAvailable = 80,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[7],
                        Name = "Grapes",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Grapes",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[8],
                        Name = "Watermelon",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Watermelon",
                        QuantityAvailable = 20,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new () {
                        Id = productIds[9],
                        Name = "Raisins",
                        CategoryId = categoryIds[4],
                        Price = rnd.Next(10, 540),
                        Description = "Raisins",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[10],
                        Name = "Orange Juice",
                        CategoryId = categoryIds[8],
                        Price = rnd.Next(10, 540),
                        Description = "Orange Juice",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[11],
                        Name = "Mixed Fruits",
                        CategoryId = categoryIds[3],
                        Price = rnd.Next(10, 540),
                        Description = "Mixed Fruits",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[12],
                        Name = "Spinach",
                        CategoryId = categoryIds[2],
                        Price = rnd.Next(10, 540),
                        Description = "Spinach",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[13],
                        Name = "Bell Pepper",
                        CategoryId = categoryIds[2],
                        Price = rnd.Next(10, 540),
                        Description = "Bell Pepper",
                        QuantityAvailable = 100,
                        CreatedDate = DateTime.UtcNow,
                    },
                    new() {
                        Id = productIds[14],
                        Name = "Fried Chicken",
                        CategoryId = categoryIds[7],
                        Price = rnd.Next(10, 540),
                        Description = "Fried Chicken",
                        QuantityAvailable = 20,
                        CreatedDate = DateTime.UtcNow,
                    }
        });

        modelBuilder.Entity<ProductImage>().HasData(
            new List<ProductImage>
            {
                    new() { Id = Guid.NewGuid() ,ProductId = productIds[0], ImageUrl = "/theme/img/product/discount/pd-3.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[1], ImageUrl = "/theme/img/product/discount/pd-4.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[2], ImageUrl = "/theme/img/product/discount/pd-5.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[3], ImageUrl = "/theme/img/product/product-1.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[4], ImageUrl = "/theme/img/product/product-2.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[5], ImageUrl = "/theme/img/product/product-3.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[6], ImageUrl = "/theme/img/product/product-8.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[7], ImageUrl = "/theme/img/product/product-4.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[8], ImageUrl = "/theme/img/product/product-7.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[9], ImageUrl = "/theme/img/product/product-9.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[10], ImageUrl = "/theme/img/product/product-11.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[11], ImageUrl = "/theme/img/product/product-12.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[12], ImageUrl = "/theme/img/latest-product/lp-1.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[13], ImageUrl = "/theme/img/product/details/product-details-2.jpg", CreatedDate = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), ProductId = productIds[14], ImageUrl = "/theme/img/product/product-10.jpg", CreatedDate = DateTime.UtcNow }
            }
        );


    }
}