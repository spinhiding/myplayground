using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Playground2.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Playground2.Data
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context =
                    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>();




                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories.Select(c => c.Value));
                }

                if (!context.Icecreams.Any())
                {
                    context.AddRange
                    (
                        new Icecream
                        {
                            Cost = 10,
                            Category = Categories["Ice"],
                        },
                        new Icecream
                        {
                            Cost = 20,
                            Category = Categories["Ice"],
                        }
                    );
                }
                context.SaveChanges();
            }
        }
        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { Name = "Drink"},
                        new Category { Name = "Ice"}
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.Name, genre);
                    }
                }
                return categories;
            }
        }
    }
}
