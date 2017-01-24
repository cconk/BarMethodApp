using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarMethodApp.Infrastructure;
using BarMethodApp.Models;
using BarMethodApp.Data;
using Microsoft.EntityFrameworkCore;
using BarMethodApp.ViewModels;

namespace BarMethodApp.Services
{
    public class BMService
    {
        private IGenericRepository _repo;

        public BMService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<BarClassVM> ListBarClasses()
        {
            var BarClasses = (from b in _repo.Query<BarClass>()
                             select new BarClassVM()
                             {
                                 Date = b.Date,
                                 Exercises = (from e in b.Exercises
                                              select new ExerciseVM()
                                              {
                                                  Description = e.Description,
                                                  Name = e.Name,
                                                  Type = e.Type
                                              }).ToList(),
                                 Instructor = b.Instructor,
                                 Name = b.Name,
                                 Type = b.Type
                             }).ToList();
            return BarClasses;
            //return _repo.Query<BarClass>().Include(bc => bc.Exercises).ToList();
        }

        public BarClassVM FindBarClass()
        {
            var selectedBarClass = (from b in _repo.Query<BarClass>() where b.Id == id select new BarClassVM() {
                Date = b.Date,
                Exercises = (from e in b.Exercises select new ExerciseVM()
                    {
                        Description = e.Description,
                        Name = e.Name,
                        Type = e.Type
                    }).ToList(),
                Instructor = b.Instructor,
                Name = b.Name,
                Type = b.Type
            }).FirstOrDefault();
            return selectedBarClass;
        }

        public void CreateBarClass(BarClassVM barClass)
        {
            //take barclassvm and send to barClass model
            var newBarClass = new BarClass
            {
                Instructor = barClass.Instructor,
                Name = barClass.Name,
                Date = barClass.Date,
                Type = barClass.Type
            };
               
            _repo.Add(newBarClass);
        }

        public void UpdateBarClass(BarClassVM barClass)
        {
            // query and a where clause using a combination of properties to uniquely identify classes

            var orginalBarClass = _repo.Query<BarClass>().FirstOrDefault(bc => bc.Name == barClass.Name && bc.Date == barClass.Date);
            orginalBarClass.Instructor = barClass.Instructor;
            orginalBarClass.Name = barClass.Name;
            orginalBarClass.Date = barClass.Date;
            orginalBarClass.Type = barClass.Type;
            _repo.Update(orginalBarClass);

            
        }

        public void DeleteBarClass(int id)
        {
            var classToDelete = _repo.Query<BarClass>().FirstOrDefault(i => i.Id == id);
            _repo.Delete<BarClass>(classToDelete);
            //_db.BarClasses.Remove(barClass);
            //_db.SaveChanges();
        }

        public IList<ExerciseVM> ListExercises()
        {
            var exercises = (from e in _repo.Query<Exercise>() select new ExerciseVM()
            {
                Description = e.Description,
                Name = e.Name,
                Type = e.Type,
            }).ToList();
            return exercises;
        }

        public Exercise FindExercise(int selectedBarClassId, int id)
        {
            var selectedBarClass = _repo.Query<BarClass>().FirstOrDefault(bc => bc.Id == selectedBarClassId);
            return _repo.Query<Exercise>().FirstOrDefault(i => i.Id == id);
        }

        public void CreateExercise(int id, Exercise exercise)
        {
            //var selectedBarClass = _repo.Query<BarClass>().FirstOrDefault(bc => bc.Id == id);

            _repo.AddExercise(id, exercise);
            _repo.SaveChanges();
        }

        public void UpdateExercise(Exercise exercise)
        {
            var originalExercise = _repo.Query<Exercise>().FirstOrDefault(ex => ex.Id == exercise.Id);
            originalExercise.Description = exercise.Description;
            originalExercise.Name = exercise.Name;
            originalExercise.Type = exercise.Type;
            _repo.SaveChanges();
        }

        public void DeleteExercise(int id)
        {
            var exercise = _repo.Query<Exercise>().FirstOrDefault(ex => ex.Id == id);
            _repo.Delete(exercise);
            _repo.SaveChanges();
        }
    }
}
