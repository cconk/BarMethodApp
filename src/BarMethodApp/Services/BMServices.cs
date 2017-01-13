using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarMethodApp.Infrastructure;
using BarMethodApp.Models;
using BarMethodApp.Data;


namespace BarMethodApp.Services
{
    public class BMServices
    {
        private IGenericRepository _repo;

        public BMServices(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<BarClass> ListBarClasses()
        {
            return (from bc in _repo.Query<BarClass>() select bc).ToList();
        }

        public BarClass FindBarClass(int id)
        {
            var selectedBarClass = (from bc in _repo.Query<BarClass>() where bc.Id == id select bc).FirstOrDefault();
            return selectedBarClass;
        }

        public void CreateBarClass(BarClass barClass)
        {
            _repo.Add(barClass);
        }

        public void UpdateBarClass(BarClass barClass)
        {
            var orginalBarClass = _repo.Query<BarClass>().FirstOrDefault(bc => bc.Id == barClass.Id);
            orginalBarClass.Instructor = barClass.Instructor;
            orginalBarClass.Name = barClass.Name;
            orginalBarClass.Date = barClass.Date;
            orginalBarClass.Type = barClass.Type;
            _repo.Update(orginalBarClass);
            
            //orginalBarClass.Instructor = barClass.Instructor;
            //orginalBarClass.Name = barClass.Name;
            //orginalBarClass.Date = barClass.Date;
            //orginalBarClass.Type = barClass.Type;
        }

        public void DeleteBarClass(int id)
        {
            var classToDelete = _repo.Query<BarClass>().FirstOrDefault(i => i.Id == id);
            _repo.Delete<BarClass>(classToDelete);
            //_db.BarClasses.Remove(barClass);
            //_db.SaveChanges();
        }

        public IList<Exercise> ListExercises()
        {
            return _repo.Query<Exercise>().ToList();
        }

        public Exercise FindExercise(int selectedBarClassId, int id)
        {
            var selectedBarClass = _repo.Query<BarClass>().FirstOrDefault(bc => bc.Id == selectedBarClassId);
            return selectedBarClass.Exercises.FirstOrDefault(i => i.Id == id);
        }

        public void CreateExercise(int id, Exercise exercise)
        {
            var selectedBarClass = _repo.Query<BarClass>().Where(bc => bc.Id == id).FirstOrDefault();
            selectedBarClass.Exercises.Add(exercise);
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
