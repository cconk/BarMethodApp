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
    public class BarMethodClassesController : Controller
    {
        //name controllers after related model pluralized
        private BMService _bmService;

        public BarMethodClassesController(BMService bmService)
        {
            _bmService = bmService;
        }

        //private ApplicationDbContext _db;
        //public BarMethodViewController(ApplicationDbContext db)
        //{
        //    this._db = db;
        //}

        // GET: api/values
        [HttpGet]
        public IList<BarClassVM> Get()
        {
            return _bmService.ListBarClasses();

            //var exercises = _db.BarClasses.Include(bc => bc.Exercises).ToList();
            //return exercises;    
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get()
        { 
            var barClass = _bmService.FindBarClass(id);
            if (barClass == null)
            {
                return NotFound();
            }
            return Ok(barClass);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]BarClassVM barClass)
        {
 
            if(!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (barClass.Name == "")
            {
                //add Bar Class
                _bmService.CreateBarClass(barClass);
                //_db.SaveChanges();
            }
            else
            {
                //update existing class

                _bmService.UpdateBarClass(barClass);

                //var orginalBarClass = _db.BarClasses.FirstOrDefault(bc => bc.Id == barClass.Id);
                //orginalBarClass.Instructor = barClass.Instructor;
                //orginalBarClass.Name = barClass.Name;
                //orginalBarClass.Date = barClass.Date;
                //orginalBarClass.Type = barClass.Type;
                //_db.SaveChanges();

            }
            return Ok(barClass);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var barClass = _bmService.FindBarClass(id);
            if (barClass == null)
            {
                return NotFound();
            }
            _bmService.DeleteBarClass(id);
            //_db.SaveChanges();
            return Ok();
        }
    }
 
}
