using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using BarMethodApp.Models;
using System.Collections.Generic;

namespace BarMethodApp.Data
{
    public class SampleData
    {
        public async static Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>(); //could change applicationuser to instructor

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure chad (IsAdmin)
            var chad = await userManager.FindByNameAsync("chadconklin@barmethod.com");
            if (chad == null)
            {
                // create user
                chad = new ApplicationUser
                {
                    UserName = "chadconklin@barmethod.com",
                    Email = "chadconklin@barmethod.com",
                    BarMethodClasses = new List<BarMethodClass>
                    {
                        new BarMethodClass { Name = "Class 1", Date = DateTime.Parse("2017-1-1"), Type = "Level 1"  },
                        new BarMethodClass { Name = "Class 2", Date = DateTime.Parse("2017-1-2"), Type = "Level 2"  },
                        new BarMethodClass { Name = "Class 3", Date = DateTime.Parse("2017-1-3"), Type = "Mixed Level"  }
                    }
                };
                await userManager.CreateAsync(chad, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(chad, new Claim("IsAdmin", "true"));
            }

            // Ensure sherrie (not IsAdmin)
            var sherrie = await userManager.FindByNameAsync("sherrieconklin@barmethod.com");
            if (sherrie == null)
            {
                // create user
                sherrie = new ApplicationUser
                {
                    UserName = "sherrieconklin@barmethod.com",
                    Email = "sherrieconklin@barmethod.com",
                    BarMethodClasses = new List<BarMethodClass>
                    {
                        new BarMethodClass { Name = "Class 4", Date = DateTime.Parse("2017-1-4"), Type = "Level 1" },
                        new BarMethodClass { Name = "Class 5", Date = DateTime.Parse("2017-1-5"), Type = "Level 2" },
                        new BarMethodClass { Name = "Class 6", Date = DateTime.Parse("2017-1-6"), Type = "Mixed Level" }
                    }
                };
                await userManager.CreateAsync(sherrie, "Secret234!");
            }

            // Ensure mary (not IsAdmin)
            var mary = await userManager.FindByNameAsync("maryconklin@barmethod.com");
            if (mary == null)
            {
                // create user
                mary = new ApplicationUser
                {
                    UserName = "maryconklin@barmethod.com",
                    Email = "maryconklin@barmethod.com",
                    BarMethodClasses = new List<BarMethodClass>
                    {
                        new BarMethodClass { Name = "Class 7", Date = DateTime.Parse("2017-1-7"), Type = "Level 1" },
                        new BarMethodClass { Name = "Class 8", Date = DateTime.Parse("2017-1-8"), Type = "Level 2"  },
                        new BarMethodClass { Name = "Class 9", Date = DateTime.Parse("2017-1-9"), Type = "Mixed Level" }
                    }
                };
                await userManager.CreateAsync(mary, "Secret345!");
            }

            // Ensure kerrianne (not IsAdmin)
            var kerrianne = await userManager.FindByNameAsync("kerriannethronson@barmethod.com");
            if (kerrianne == null)
            {
                // create user
                kerrianne = new ApplicationUser
                {
                    UserName = "kerriannethronson@barmethod.com",
                    Email = "kerriannethronson@barmethod.com",
                    BarMethodClasses = new List<BarMethodClass>
                    {
                        new BarMethodClass { Name = "Class 10", Date = DateTime.Parse("2017-1-10"), Type = "Level 1" },
                        new BarMethodClass { Name = "Class 11", Date = DateTime.Parse("2017-1-11"), Type = "Level 2" },
                        new BarMethodClass { Name = "Class 12", Date = DateTime.Parse("2017-1-12"), Type = "Mixed Level" }
                    }
                };
                await userManager.CreateAsync(kerrianne, "Secret456!");
            }

            var db = serviceProvider.GetService<ApplicationDbContext>();
            await db.Database.EnsureCreatedAsync();

            db.SaveChanges();


            //if (!db.BarMethodClasses.Any())
            //{
            //    db.BarMethodClasses.AddRange(
            //        new BarMethodClass { Name = "Class 1", Date = DateTime.Parse("2017-1-1"), Type = "Level 1" },
            //        new BarMethodClass { Name = "Class 2", Date = DateTime.Parse("2017-1-2"), Type = "Level 2" },
            //        new BarMethodClass { Name = "Class 3", Date = DateTime.Parse("2017-1-3"), Type = "Mixed Level" },
            //        new BarMethodClass { Name = "Class 4", Date = DateTime.Parse("2017-1-4"), Type = "Level 1" },
            //        new BarMethodClass { Name = "Class 5", Date = DateTime.Parse("2017-1-4"), Type = "Level 2" },
            //        new BarMethodClass { Name = "Class 6", Date = DateTime.Parse("2017-1-6"), Type = "Mixed Level" }
            //        );
            //    db.SaveChanges();
            //}

            if (!db.Exercises.Any())
            {
                db.Exercises.AddRange(
                    new Exercise { Name = "Exercise 1", Description = "Good Exercise 1", Order = 1, Type = "Hard" },
                    new Exercise { Name = "Exercise 2", Description = "Good Exercise 2", Order = 2, Type = "Moderate" },
                    new Exercise { Name = "Exercise 3", Description = "Good Exercise 3", Order = 3, Type = "Easy" },
                    new Exercise { Name = "Exercise 4", Description = "Good Exercise 4", Order = 1, Type = "Hard" },
                    new Exercise { Name = "Exercise 5", Description = "Good Exercise 5", Order = 2, Type = "Moderate" },
                    new Exercise { Name = "Exercise 6", Description = "Good Exercise 6", Order = 3, Type = "Easy" },
                    new Exercise { Name = "Exercise 7", Description = "Good Exercise 7", Order = 1, Type = "Hard" },
                    new Exercise { Name = "Exercise 8", Description = "Good Exercise 8", Order = 2, Type = "Moderate" },
                    new Exercise { Name = "Exercise 9", Description = "Good Exercise 9", Order = 3, Type = "Easy" },
                    new Exercise { Name = "Exercise 10", Description = "Good Exercise 10", Order = 1, Type = "Hard" },
                    new Exercise { Name = "Exercise 11", Description = "Good Exercise 11", Order = 2, Type = "Moderate" },
                    new Exercise { Name = "Exercise 12", Description = "Good Exercise 12", Order = 3, Type = "Easy" }
                    );
                db.SaveChanges();
            }

            if (!db.BMCExercises.Any())
            {
                db.BMCExercises.AddRange(
                    new BMCExercise { BMCId = 1, ExerciseId = 1 },
                    new BMCExercise { BMCId = 1, ExerciseId = 2 },
                    new BMCExercise { BMCId = 1, ExerciseId = 3 },
                    new BMCExercise { BMCId = 2, ExerciseId = 1 },
                    new BMCExercise { BMCId = 2, ExerciseId = 2 },
                    new BMCExercise { BMCId = 2, ExerciseId = 3 },
                    new BMCExercise { BMCId = 3, ExerciseId = 1 },
                    new BMCExercise { BMCId = 3, ExerciseId = 2 },
                    new BMCExercise { BMCId = 3, ExerciseId = 3 },
                    new BMCExercise { BMCId = 4, ExerciseId = 4 },
                    new BMCExercise { BMCId = 4, ExerciseId = 5 },
                    new BMCExercise { BMCId = 4, ExerciseId = 6 },
                    new BMCExercise { BMCId = 5, ExerciseId = 4 },
                    new BMCExercise { BMCId = 5, ExerciseId = 5 },
                    new BMCExercise { BMCId = 5, ExerciseId = 6 },
                    new BMCExercise { BMCId = 6, ExerciseId = 4 },
                    new BMCExercise { BMCId = 6, ExerciseId = 5 },
                    new BMCExercise { BMCId = 6, ExerciseId = 6 },
                    new BMCExercise { BMCId = 7, ExerciseId = 7 },
                    new BMCExercise { BMCId = 7, ExerciseId = 8 },
                    new BMCExercise { BMCId = 7, ExerciseId = 9 },
                    new BMCExercise { BMCId = 8, ExerciseId = 7 },
                    new BMCExercise { BMCId = 8, ExerciseId = 8 },
                    new BMCExercise { BMCId = 8, ExerciseId = 9 },
                    new BMCExercise { BMCId = 9, ExerciseId = 7 },
                    new BMCExercise { BMCId = 9, ExerciseId = 8 },
                    new BMCExercise { BMCId = 9, ExerciseId = 9 },
                    new BMCExercise { BMCId = 10, ExerciseId = 10 },
                    new BMCExercise { BMCId = 10, ExerciseId = 11 },
                    new BMCExercise { BMCId = 10, ExerciseId = 12 },
                    new BMCExercise { BMCId = 11, ExerciseId = 10 },
                    new BMCExercise { BMCId = 11, ExerciseId = 11 },
                    new BMCExercise { BMCId = 11, ExerciseId = 12 },
                    new BMCExercise { BMCId = 12, ExerciseId = 10 },
                    new BMCExercise { BMCId = 12, ExerciseId = 11 },
                    new BMCExercise { BMCId = 12, ExerciseId = 12 }
                    );
                db.SaveChanges();
            }

        }

    }
}
