using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarMethodApp.Models;
using BarMethodApp.Data;
using Microsoft.EntityFrameworkCore;
using BarMethodApp.Services;
using BarMethodApp.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BarMethodApp.API
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        private ExercisesService  _exerciseService;

        public ExerciseController(ExercisesService exerciseService)

        {
            _exerciseService = exerciseService;
        }

        //private ApplicationDbContext _db;
        //public ExerciseController(ApplicationDbContext db)
        //{
        //    this._db = db;
        //}

        // GET: api/values
        [HttpGet]
        public IList<ExerciseVM> Get()
        {
            
            return _exerciseService.ListExercises();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IList<ExerciseVM> Get(int id)
        {
            //var selectedBarClass = _bmService.FindBarClass(selectedBarClassId);

            //if (selectedBarClass == null)
            //{
            //    return NotFound();
            //}

            //var exercise = _bmService.FindExercise(id);
            //if (exercise == null)
            //{
            //    return NotFound();
            //}
            //return Ok(exercise);

            //var exercise = _bmService.FindExercise(selectedBarClassId, id);
            return _exerciseService.ListExercisesByBarClass(id);
        }

        // POST api/values
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]Exercise exercise)
        {
            //var selectedBarClass = _bmService.FindBarClass(id);
            //selectedBarClass.Exercises.Add(exercise);

            //_bmService.CreateExercise(id, exercise);
            _exerciseService.AddExercise(id, exercise);
            return Ok();

            //return _bmService.CreateExercise(id, exercise);
        }

        // PUT api/values/5
        [HttpPost]
        public IActionResult Post([FromBody]Exercise exercise)
        {

            _exerciseService.UpdateExercise(exercise);
            //var originalExercise = _db.Exercises.FirstOrDefault(ex => ex.Id == exercise.Id);
            //originalExercise.Description = exercise.Description;
            //originalExercise.Name = exercise.Name;
            //originalExercise.Type = exercise.Type;
            //_db.SaveChanges();
        
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var exercise = _bmService.FindExercise();
            if (id == 0)
            {
                return NotFound();
            }
            //_bmService.DeleteExercise(id);
            //_db.Exercises.Remove(exercise);
            //_db.SaveChanges();
            return Ok();
        }
    }
}
