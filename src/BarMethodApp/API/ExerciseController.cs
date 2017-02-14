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
            return _exerciseService.ListExercisesByBarClass(id);
        }

        // POST api/values
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            else
            { 
                _exerciseService.AddExercise(id, exercise);
                return Ok();
            }
        }

        // PUT api/values/5
        [HttpPost]
        public IActionResult Post([FromBody]Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            else
            { 
                _exerciseService.UpdateExercise(exercise);
                return Ok();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            else
            {
                _exerciseService.DeleteExercise(id);
                return Ok();
            }
            
        }
    }
}
