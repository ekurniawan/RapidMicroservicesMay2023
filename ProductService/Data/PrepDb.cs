using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                Console.WriteLine("--> Seeding data...");
                context.Products.AddRange(
                    new Product() { Name = "Kopi Arabica Toraja", Stock = 12, Description = "Kopi Arabica Toraja Single Origin 250g", Price = 75000 },
                    new Product() { Name = "Kopi Aceh Gayo", Stock = 20, Description = "Kopi Aceh Gayo Single Origin 250g", Price = 90000 },
                    new Product() { Name = "Kopi Aceh Gayo Wine", Stock = 30, Description = "Kopi Aceh Gayo Wine Honey wash 250g", Price = 120000 }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}