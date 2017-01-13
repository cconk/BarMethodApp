using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarMethodApp.Models;
using BarMethodApp.Data;
using Microsoft.EntityFrameworkCore;
using BarMethodApp.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BarMethodApp.API
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        private BMServices _bmService;

        public ExerciseController(BMServices bmService)
        {
            _bmService = bmService;
        }

        //private ApplicationDbContext _db;
        //public ExerciseController(ApplicationDbContext db)
        //{
        //    this._db = db;
        //}

        // GET: api/values
        [HttpGet]
        public IEnumerable<Exercise> Get()
        {
            var exercises = _bmService.ListExercises();
            return exercises;
        }

        // GET api/values/5
        [HttpGet("{selectedBarClassId}/exercises/{id}", Name = "GetExercise")]
        public IActionResult GetExercises(int selectedBarClassId, int id)
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

            var exercise = _bmService.FindExercise(selectedBarClassId, id);
            return Ok(exercise);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(int id, [FromBody]Exercise exercise)
        {
            //var selectedBarClass = _bmService.FindBarClass(id);
            //selectedBarClass.Exercises.Add(exercise);
            
            _bmService.CreateExercise(id, exercise);
            return Ok();

            //return _bmService.CreateExercise(id, exercise);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Exercise exercise)
        {

            _bmService.UpdateExercise(exercise);
            //var originalExercise = _db.Exercises.FirstOrDefault(ex => ex.Id == exercise.Id);
            //originalExercise.Description = exercise.Description;
            //originalExercise.Name = exercise.Name;
            //originalExercise.Type = exercise.Type;
            //_db.SaveChanges();
        
            return Ok(exercise);
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
            _bmService.DeleteExercise(id);
            //_db.Exercises.Remove(exercise);
            //_db.SaveChanges();
            return Ok();
        }
    }
}
