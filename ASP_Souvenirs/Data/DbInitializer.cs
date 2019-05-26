using ASP_Souvenirs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Souvenirs.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any bags.
            if (context.Sourvenirs.Any())
            {
                return; //DB has been seeded
            }

            var categories = new Category[] {
                new Category {Name = "Maori Gifts"},
                new Category {Name = "T-Shirts"},
                new Category {Name = "Mugs"}
            };

            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

        }
    }
}
