using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using BarMethodApp.Models;


namespace BarMethodApp.Data
{
    public class SampleData
    {
        public async static Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<ApplicationDbContext>();
            await db.Database.EnsureCreatedAsync();

            if (!db.BarClasses.Any())
            {
                db.BarClasses.AddRange(
                    new BarClass { Name = "Level 2", Instructor = "Sherrie Conklin" },
                    new BarClass { Name = "Level 1", Instructor = "Mary Conklin" },
                    new BarClass { Name = "Bar Express", Instructor = "Kerrianne Thronson" },
                    new BarClass { Name = "Bar Move", Instructor = "Sherrie Conklin" }
                );
                db.SaveChanges();
            }
        }
    }
}
