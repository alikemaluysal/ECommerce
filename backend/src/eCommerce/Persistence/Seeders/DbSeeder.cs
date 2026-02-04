using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NArchitecture.Core.Security.Hashing;
using Persistence.Contexts;

namespace Persistence.Seeders;

public class DbSeeder
{
    private readonly BaseDbContext _context;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(BaseDbContext context, ILogger<DbSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedOperationClaimsAsync();
            await SeedUsersAsync();
            await SeedCategoriesAsync();
            await SeedProductsAsync();
            await SeedOrdersAsync();
            
            _logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedOperationClaimsAsync()
    {
        if (await _context.OperationClaims.AnyAsync())
            return;

        var operationClaims = new List<OperationClaim>
        {
            new() { Name = "Admin", CreatedDate = DateTime.UtcNow },
            new() { Name = "User", CreatedDate = DateTime.UtcNow }
        };

        await _context.OperationClaims.AddRangeAsync(operationClaims);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Operation claims seeded successfully.");
    }

    private async Task SeedUsersAsync()
    {
        if (await _context.Users.AnyAsync())
            return;

        HashingHelper.CreatePasswordHash("112", out byte[] passwordHash, out byte[] passwordSalt);

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@alikemaluysal.com",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Users.AddAsync(adminUser);
        await _context.SaveChangesAsync();

        var adminClaim = await _context.OperationClaims.FirstOrDefaultAsync(c => c.Name == "Admin");
        if (adminClaim != null)
        {
            var userOperationClaim = new UserOperationClaim
            {
                Id = Guid.NewGuid(),
                UserId = adminUser.Id,
                OperationClaimId = adminClaim.Id,
                CreatedDate = DateTime.UtcNow
            };
            await _context.UserOperationClaims.AddAsync(userOperationClaim);
            await _context.SaveChangesAsync();
        }

        _logger.LogInformation("Users seeded successfully.");
    }

    private async Task SeedCategoriesAsync()
    {
        if (await _context.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Electronics", Description = "Tech gadgets and devices", CreatedDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Fashion", Description = "Clothing and accessories", CreatedDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Home & Living", Description = "Home decor and furniture", CreatedDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Sports", Description = "Sports and fitness equipment", CreatedDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Beauty", Description = "Beauty and personal care", CreatedDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Books", Description = "Books and magazines", CreatedDate = DateTime.UtcNow }
        };

        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Categories seeded successfully.");
    }

    private async Task SeedProductsAsync()
    {
        if (await _context.Products.AnyAsync())
            return;

        var categories = await _context.Categories.ToListAsync();
        var electronics = categories.FirstOrDefault(c => c.Name == "Electronics");
        var fashion = categories.FirstOrDefault(c => c.Name == "Fashion");
        var homeLiving = categories.FirstOrDefault(c => c.Name == "Home & Living");
        var sports = categories.FirstOrDefault(c => c.Name == "Sports");
        var beauty = categories.FirstOrDefault(c => c.Name == "Beauty");

        if (electronics == null || fashion == null || homeLiving == null || sports == null || beauty == null)
            return;

        var products = new List<Product>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Premium Wireless Headphones Pro",
                Description = "High-fidelity audio with active noise cancellation. Experience crystal-clear sound quality with up to 30 hours of battery life. Perfect for music lovers and professionals.",
                Price = 299,
                Stock = 24,
                CategoryId = electronics.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Minimalist Leather Wallet",
                Description = "Handcrafted genuine leather wallet with RFID protection. Slim design holds up to 8 cards and cash. Perfect for everyday carry.",
                Price = 89,
                Stock = 45,
                CategoryId = fashion.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Smart Watch Pro",
                Description = "Advanced fitness tracking and smart notifications. Heart rate monitor, GPS, and waterproof design. Compatible with iOS and Android.",
                Price = 449,
                Stock = 18,
                CategoryId = electronics.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Designer Sunglasses",
                Description = "Premium UV protection with polarized lenses. Stylish design for any occasion. Includes protective case and cleaning cloth.",
                Price = 179,
                Stock = 32,
                CategoryId = fashion.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Organic Cotton T-Shirt",
                Description = "Sustainable and comfortable everyday wear. Made from 100% organic cotton. Available in multiple colors.",
                Price = 39,
                Stock = 156,
                CategoryId = fashion.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Ceramic Coffee Mug Set",
                Description = "Set of 4 handcrafted ceramic mugs. Microwave and dishwasher safe. Perfect gift for coffee lovers.",
                Price = 49,
                Stock = 67,
                CategoryId = homeLiving.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Yoga Mat Premium",
                Description = "Extra thick non-slip yoga mat. Eco-friendly TPE material. Includes carrying strap. Perfect for yoga and pilates.",
                Price = 59,
                Stock = 43,
                CategoryId = sports.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Stainless Steel Water Bottle",
                Description = "Insulated water bottle keeps drinks cold for 24 hours or hot for 12 hours. BPA-free and leak-proof design.",
                Price = 29,
                Stock = 234,
                CategoryId = sports.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Mechanical Keyboard RGB",
                Description = "Professional gaming keyboard with RGB backlighting. Cherry MX switches for superior tactile feedback. Programmable keys and macro support.",
                Price = 159,
                Stock = 34,
                CategoryId = electronics.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Portable Bluetooth Speaker",
                Description = "360-degree sound with deep bass. Waterproof and dustproof IP67 rating. 20-hour battery life for outdoor adventures.",
                Price = 129,
                Stock = 56,
                CategoryId = electronics.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Running Shoes Pro",
                Description = "Lightweight running shoes with responsive cushioning. Breathable mesh upper and durable rubber outsole. Perfect for daily training.",
                Price = 139,
                Stock = 78,
                CategoryId = sports.Id,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Skincare Essentials Kit",
                Description = "Complete skincare routine in one kit. Includes cleanser, toner, serum, and moisturizer. Suitable for all skin types.",
                Price = 89,
                Stock = 123,
                CategoryId = beauty.Id,
                CreatedDate = DateTime.UtcNow
            }
        };

        await _context.Products.AddRangeAsync(products);
        await _context.SaveChangesAsync();

        await SeedProductImagesAsync(products);
        await SeedProductSpecificationsAsync(products);

        _logger.LogInformation("Products seeded successfully.");
    }

    private async Task SeedProductImagesAsync(List<Product> products)
    {
        var images = new List<ProductImage>();

        var headphones = products.FirstOrDefault(p => p.Name == "Premium Wireless Headphones Pro");
        if (headphones != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = headphones.Id,
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = headphones.Id,
                ImageUrl = "https://images.unsplash.com/photo-1484704849700-f032a568e944?w=500",
                IsPrimary = false,
                DisplayOrder = 1,
                CreatedDate = DateTime.UtcNow
            });
        }

        var wallet = products.FirstOrDefault(p => p.Name == "Minimalist Leather Wallet");
        if (wallet != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = wallet.Id,
                ImageUrl = "https://images.unsplash.com/photo-1627123424574-724758594e93?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var smartWatch = products.FirstOrDefault(p => p.Name == "Smart Watch Pro");
        if (smartWatch != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = smartWatch.Id,
                ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = smartWatch.Id,
                ImageUrl = "https://images.unsplash.com/photo-1579586337278-3befd40fd17a?w=500",
                IsPrimary = false,
                DisplayOrder = 1,
                CreatedDate = DateTime.UtcNow
            });
        }

        var sunglasses = products.FirstOrDefault(p => p.Name == "Designer Sunglasses");
        if (sunglasses != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = sunglasses.Id,
                ImageUrl = "https://images.unsplash.com/photo-1572635196237-14b3f281503f?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var tshirt = products.FirstOrDefault(p => p.Name == "Organic Cotton T-Shirt");
        if (tshirt != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = tshirt.Id,
                ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var mugSet = products.FirstOrDefault(p => p.Name == "Ceramic Coffee Mug Set");
        if (mugSet != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = mugSet.Id,
                ImageUrl = "https://images.unsplash.com/photo-1514228742587-6b1558fcca3d?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var yogaMat = products.FirstOrDefault(p => p.Name == "Yoga Mat Premium");
        if (yogaMat != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = yogaMat.Id,
                ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var waterBottle = products.FirstOrDefault(p => p.Name == "Stainless Steel Water Bottle");
        if (waterBottle != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = waterBottle.Id,
                ImageUrl = "https://images.unsplash.com/photo-1602143407151-7111542de6e8?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var keyboard = products.FirstOrDefault(p => p.Name == "Mechanical Keyboard RGB");
        if (keyboard != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = keyboard.Id,
                ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var speaker = products.FirstOrDefault(p => p.Name == "Portable Bluetooth Speaker");
        if (speaker != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = speaker.Id,
                ImageUrl = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var runningShoes = products.FirstOrDefault(p => p.Name == "Running Shoes Pro");
        if (runningShoes != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = runningShoes.Id,
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        var skincareKit = products.FirstOrDefault(p => p.Name == "Skincare Essentials Kit");
        if (skincareKit != null)
        {
            images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = skincareKit.Id,
                ImageUrl = "https://images.unsplash.com/photo-1556228720-195a672e8a03?w=500",
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedDate = DateTime.UtcNow
            });
        }

        if (images.Any())
        {
            await _context.ProductImages.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }
    }

    private async Task SeedProductSpecificationsAsync(List<Product> products)
    {
        var specifications = new List<ProductSpecification>();

        var headphones = products.FirstOrDefault(p => p.Name == "Premium Wireless Headphones Pro");
        if (headphones != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = headphones.Id, Key = "Battery Life", Value = "30 hours", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = headphones.Id, Key = "Connectivity", Value = "Bluetooth 5.0", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = headphones.Id, Key = "Weight", Value = "250g", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = headphones.Id, Key = "Noise Cancellation", Value = "Active", CreatedDate = DateTime.UtcNow }
            });
        }

        var wallet = products.FirstOrDefault(p => p.Name == "Minimalist Leather Wallet");
        if (wallet != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = wallet.Id, Key = "Material", Value = "Genuine Leather", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = wallet.Id, Key = "RFID Protection", Value = "Yes", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = wallet.Id, Key = "Card Slots", Value = "8", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = wallet.Id, Key = "Dimensions", Value = "11cm x 9cm", CreatedDate = DateTime.UtcNow }
            });
        }

        var smartWatch = products.FirstOrDefault(p => p.Name == "Smart Watch Pro");
        if (smartWatch != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = smartWatch.Id, Key = "Display", Value = "1.4\" AMOLED", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = smartWatch.Id, Key = "Battery", Value = "7 days", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = smartWatch.Id, Key = "Water Resistance", Value = "5ATM", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = smartWatch.Id, Key = "GPS", Value = "Built-in", CreatedDate = DateTime.UtcNow }
            });
        }

        var sunglasses = products.FirstOrDefault(p => p.Name == "Designer Sunglasses");
        if (sunglasses != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = sunglasses.Id, Key = "UV Protection", Value = "100%", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = sunglasses.Id, Key = "Lens Type", Value = "Polarized", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = sunglasses.Id, Key = "Frame Material", Value = "Acetate", CreatedDate = DateTime.UtcNow }
            });
        }

        var tshirt = products.FirstOrDefault(p => p.Name == "Organic Cotton T-Shirt");
        if (tshirt != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = tshirt.Id, Key = "Material", Value = "100% Organic Cotton", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = tshirt.Id, Key = "Fit", Value = "Regular", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = tshirt.Id, Key = "Care", Value = "Machine washable", CreatedDate = DateTime.UtcNow }
            });
        }

        var mugs = products.FirstOrDefault(p => p.Name == "Ceramic Coffee Mug Set");
        if (mugs != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = mugs.Id, Key = "Quantity", Value = "4 mugs", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = mugs.Id, Key = "Capacity", Value = "350ml each", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = mugs.Id, Key = "Material", Value = "Ceramic", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = mugs.Id, Key = "Dishwasher Safe", Value = "Yes", CreatedDate = DateTime.UtcNow }
            });
        }

        var yogaMat = products.FirstOrDefault(p => p.Name == "Yoga Mat Premium");
        if (yogaMat != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = yogaMat.Id, Key = "Thickness", Value = "6mm", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = yogaMat.Id, Key = "Material", Value = "TPE", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = yogaMat.Id, Key = "Dimensions", Value = "183cm x 61cm", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = yogaMat.Id, Key = "Non-slip", Value = "Yes", CreatedDate = DateTime.UtcNow }
            });
        }

        var waterBottle = products.FirstOrDefault(p => p.Name == "Stainless Steel Water Bottle");
        if (waterBottle != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = waterBottle.Id, Key = "Capacity", Value = "750ml", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = waterBottle.Id, Key = "Material", Value = "Stainless Steel", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = waterBottle.Id, Key = "Insulation", Value = "Double-wall", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = waterBottle.Id, Key = "BPA-free", Value = "Yes", CreatedDate = DateTime.UtcNow }
            });
        }

        var keyboard = products.FirstOrDefault(p => p.Name == "Mechanical Keyboard RGB");
        if (keyboard != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = keyboard.Id, Key = "Switch Type", Value = "Cherry MX Red", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = keyboard.Id, Key = "RGB", Value = "Yes", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = keyboard.Id, Key = "Connectivity", Value = "USB-C", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = keyboard.Id, Key = "Programmable", Value = "Yes", CreatedDate = DateTime.UtcNow }
            });
        }

        var speaker = products.FirstOrDefault(p => p.Name == "Portable Bluetooth Speaker");
        if (speaker != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = speaker.Id, Key = "Battery Life", Value = "20 hours", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = speaker.Id, Key = "Water Resistance", Value = "IP67", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = speaker.Id, Key = "Connectivity", Value = "Bluetooth 5.0", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = speaker.Id, Key = "Output", Value = "20W", CreatedDate = DateTime.UtcNow }
            });
        }

        var shoes = products.FirstOrDefault(p => p.Name == "Running Shoes Pro");
        if (shoes != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = shoes.Id, Key = "Weight", Value = "280g", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = shoes.Id, Key = "Drop", Value = "10mm", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = shoes.Id, Key = "Upper Material", Value = "Mesh", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = shoes.Id, Key = "Cushioning", Value = "Responsive foam", CreatedDate = DateTime.UtcNow }
            });
        }

        var skincare = products.FirstOrDefault(p => p.Name == "Skincare Essentials Kit");
        if (skincare != null)
        {
            specifications.AddRange(new[]
            {
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = skincare.Id, Key = "Items", Value = "4 products", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = skincare.Id, Key = "Skin Type", Value = "All types", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = skincare.Id, Key = "Cruelty-free", Value = "Yes", CreatedDate = DateTime.UtcNow },
                new ProductSpecification { Id = Guid.NewGuid(), ProductId = skincare.Id, Key = "Vegan", Value = "Yes", CreatedDate = DateTime.UtcNow }
            });
        }

        if (specifications.Any())
        {
            await _context.ProductSpecifications.AddRangeAsync(specifications);
            await _context.SaveChangesAsync();
        }
    }

    private async Task SeedOrdersAsync()
    {
        if (await _context.Orders.AnyAsync())
            return;

        var users = await _context.Users.ToListAsync();
        var products = await _context.Products.ToListAsync();

        if (!users.Any() || !products.Any())
            return;

        var adminUser = users.FirstOrDefault(u => u.Email == "admin@alikemaluysal.com");
        if (adminUser == null)
            return;

        var headphones = products.FirstOrDefault(p => p.Name == "Premium Wireless Headphones Pro");
        var smartWatch = products.FirstOrDefault(p => p.Name == "Smart Watch Pro");
        var wallet = products.FirstOrDefault(p => p.Name == "Minimalist Leather Wallet");
        var mugs = products.FirstOrDefault(p => p.Name == "Ceramic Coffee Mug Set");
        var waterBottle = products.FirstOrDefault(p => p.Name == "Stainless Steel Water Bottle");

        var orders = new List<Order>();

        if (headphones != null && smartWatch != null)
        {
            var order1 = new Order
            {
                Id = Guid.NewGuid(),
                UserId = adminUser.Id,
                TotalAmount = 724.02m,
                Status = OrderStatus.Shipped,
                ShippingAddress = "123 Main St, Apt 4B",
                ShippingCity = "New York",
                ShippingCountry = "USA",
                ShippingPostalCode = "10001",
                CreatedDate = DateTime.UtcNow.AddDays(-5)
            };

            orders.Add(order1);
            await _context.Orders.AddAsync(order1);
            await _context.SaveChangesAsync();

            var orderItems1 = new List<OrderItem>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order1.Id,
                    ProductId = headphones.Id,
                    Quantity = 1,
                    UnitPrice = headphones.Price,
                    CreatedDate = DateTime.UtcNow.AddDays(-5)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order1.Id,
                    ProductId = smartWatch.Id,
                    Quantity = 1,
                    UnitPrice = smartWatch.Price * 0.8m,
                    CreatedDate = DateTime.UtcNow.AddDays(-5)
                }
            };

            await _context.OrderItems.AddRangeAsync(orderItems1);
        }

        if (wallet != null)
        {
            var order2 = new Order
            {
                Id = Guid.NewGuid(),
                UserId = adminUser.Id,
                TotalAmount = 112.9m,
                Status = OrderStatus.Processing,
                ShippingAddress = "456 Oak Avenue",
                ShippingCity = "Los Angeles",
                ShippingCountry = "USA",
                ShippingPostalCode = "90001",
                CreatedDate = DateTime.UtcNow.AddDays(-3)
            };

            orders.Add(order2);
            await _context.Orders.AddAsync(order2);
            await _context.SaveChangesAsync();

            var orderItems2 = new List<OrderItem>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order2.Id,
                    ProductId = wallet.Id,
                    Quantity = 1,
                    UnitPrice = wallet.Price,
                    CreatedDate = DateTime.UtcNow.AddDays(-3)
                }
            };

            await _context.OrderItems.AddRangeAsync(orderItems2);
        }

        if (mugs != null && waterBottle != null)
        {
            var order3 = new Order
            {
                Id = Guid.NewGuid(),
                UserId = adminUser.Id,
                TotalAmount = 203.5m,
                Status = OrderStatus.Delivered,
                ShippingAddress = "789 Pine Road",
                ShippingCity = "Chicago",
                ShippingCountry = "USA",
                ShippingPostalCode = "60601",
                CreatedDate = DateTime.UtcNow.AddDays(-10)
            };

            orders.Add(order3);
            await _context.Orders.AddAsync(order3);
            await _context.SaveChangesAsync();

            var orderItems3 = new List<OrderItem>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order3.Id,
                    ProductId = mugs.Id,
                    Quantity = 2,
                    UnitPrice = mugs.Price,
                    CreatedDate = DateTime.UtcNow.AddDays(-10)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order3.Id,
                    ProductId = waterBottle.Id,
                    Quantity = 3,
                    UnitPrice = waterBottle.Price,
                    CreatedDate = DateTime.UtcNow.AddDays(-10)
                }
            };

            await _context.OrderItems.AddRangeAsync(orderItems3);
        }

        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Orders seeded successfully.");
    }
}
