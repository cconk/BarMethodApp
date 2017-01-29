using BarMethodApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarMethodApp.Models;
using BarMethodApp.ViewModels;


namespace BarMethodApp.Services
{
    public class ExercisesService
    {
        private IGenericRepository _repo;

        public ExercisesService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<ExerciseVM> ListExercisesByBarClass(string name, DateTime date)
        {
            // based on user input from front end return a list of barClasses for selected instructor
            var selectedBarMethodClass = (from bc in _repo.Query<BarMethodClass>()
                                      where bc.Name == name && bc.Date == date
                                      select new BarMethodClassVM()
                                      {
                                          //Name = bc.Name,
                                          //Date = bc.Date,
                                          Exercises = (from e in bc.Exercises
                                                        select new ExerciseVM()).ToList()
                                      }).FirstOrDefault();
            var selectedExercises = selectedBarMethodClass.Exercises.ToList();
            return selectedExercises;
        }
    }
}
