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
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BarMethodApp.API
{

    [Route("api/[controller]")]
    public class BarMethodClassesController : Controller
    {
        //name controllers after related model pluralized
        private BarMethodClassesService _barMethodClassesService;

        public BarMethodClassesController(BarMethodClassesService barMethodClassesService)
        {
            _barMethodClassesService = barMethodClassesService;
        }

        // GET: api/values
        [HttpGet]
        public IList<ApplicationUserVM> Get()
        {
            return _barMethodClassesService.ListInstructors();
            //var exercises = _db.BarClasses.Include(bc => bc.Exercises).ToList();
            //return exercises;    
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public  IList<BarMethodClassVM> Get(string id)
        {
            return _barMethodClassesService.ListBarClassByInstructor(id);
        }

        // POST api/values
        [HttpPost("{id}")]
        public IActionResult Post(string id, [FromBody]BarMethodClass newBarMethodClass)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (newBarMethodClass.Name == "")
            {
                //error messaging
            }
            else
            { 
                //_barMethodClasses services method for adding a new class
                _barMethodClassesService.AddNewBarMethodClass(id, newBarMethodClass);
            }
            return Ok(newBarMethodClass);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            //var barClass = _barMethodClassesService.FindBarClass(id);
            //if (barClass == null)
            //{
            //    return NotFound();
            //}
            //_barMethodClassesService.DeleteBarClass(id);
            ////_db.SaveChanges();
            return Ok();
        }
    }
 
}
