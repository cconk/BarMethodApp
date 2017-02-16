using BarMethodApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarMethodApp.Models;
using BarMethodApp.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace BarMethodApp.Services
{
    public class ExercisesService
    {
        private IGenericRepository _repo;

        public ExercisesService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<ExerciseVM> ListExercisesByBarClass(int id)
        {
            // based on user input from front end return a list of barClasses for selected instructor
            var selectedExercises = (from bc in _repo.Query<BMCExercise>()
                                     where bc.BMCId == id
                                     select new ExerciseVM()
                                     {
                                        Id = bc.Exercise.Id,
                                        Type = bc.Exercise.Type,
                                        Name = bc.Exercise.Name,
                                        Order = bc.Exercise.Order,
                                        Description = bc.Exercise.Description
                                     }).ToList();
            _repo.SaveChanges();
            return selectedExercises;
        }

        public IList<ExerciseVM> ListExercises() // may not ever need this method
        {
            var exercises = (from e in _repo.Query<Exercise>()
                              select new ExerciseVM()
                              {
                                  Name = e.Name,
                                  Description = e.Description,
                                  Type = e.Type
                              }).ToList();
            return exercises;
        }

        public void UpdateExercise(string userName, Exercise selectedExercise)
        {
            var exercisetoUpdate = (from e in _repo.Query<Exercise>() where e.Id == selectedExercise.Id
                                    select new Exercise()
                                    {
                                        Id = e.Id,
                                        Description = e.Description,
                                        Name = e.Name,
                                        Order = e.Order,
                                        Type = e.Type,
                                        BarMethodClasses = (from b in e.BarMethodClasses select new BMCExercise()
                                        {
                                            BMClass = new BarMethodClass()
                                            {
                                                Instructor=b.BMClass.Instructor
                                            }
                                        }).ToList()
                                    }).FirstOrDefault();
            var instructor = exercisetoUpdate.BarMethodClasses[0].BMClass.Instructor;
            if (instructor.UserName == userName)
            {
                exercisetoUpdate.BarMethodClasses = null;
                exercisetoUpdate.Description = selectedExercise.Description;
                _repo.Update(exercisetoUpdate);
                _repo.SaveChanges();
            }
        }

        public void AddExercise(int id, Exercise newExercise)
        {
            var exerciseToAdd = new Exercise()
            {
                Description = newExercise.Description,
                Name = newExercise.Name,
                Type= newExercise.Type,
                Order = newExercise.Order
            };

            _repo.Add(exerciseToAdd);
            _repo.SaveChanges();

            var BMCExercise = new BMCExercise()
            {
                BMCId = id,
                ExerciseId = exerciseToAdd.Id
            };
        
            _repo.Add(BMCExercise);
            _repo.SaveChanges();

        }

        public void DeleteExercise(int id)
        {
            var exerciseToDelete = (from e in _repo.Query<Exercise>() where e.Id == id select e).FirstOrDefault();

            _repo.Delete(exerciseToDelete);
        }
    }
}
