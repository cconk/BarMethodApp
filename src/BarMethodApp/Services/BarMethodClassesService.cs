using BarMethodApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarMethodApp.ViewModels;
using BarMethodApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BarMethodApp.Services
{
    public class BarMethodClassesService
    {
        private IGenericRepository _repo;

        public BarMethodClassesService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<ApplicationUserVM> ListInstructors()
        {
            var barInstructors = (from i in _repo.Query<ApplicationUser>()
                                  select new ApplicationUserVM()
                                  {
                                      FirstName = i.FirstName,
                                      LastName = i.LastName,
                                      UserName = i.UserName,
                                  }).ToList();
            return barInstructors;
        }

        public IList<BarMethodClassVM> ListBarClassByInstructor(string userName)
        {
            
            // based on user input from front end return a list of barClasses for selected instructor
            var selectedInstructor = (from i in _repo.Query<ApplicationUser>()
                              where i.UserName == userName
                              select new ApplicationUserVM()
                              {
                                  BarMethodClasses = (from bc in i.BarMethodClasses
                                                      select new BarMethodClassVM()
                                                      {
                                                          Id = bc.Id,
                                                          Name = bc.Name,
                                                          Date = bc.Date,
                                                          Type = bc.Type
                                                      }).ToList()
                              }).FirstOrDefault();
            return selectedInstructor.BarMethodClasses;
        }

        public void AddNewBarMethodClass(string userName, BarMethodClass newBarMethodClass)
        {
            //Add a new class to an existing instructor/user
            var selectedInstructor = (from i in _repo.Query<ApplicationUser>().Include(i=>i.BarMethodClasses)
                                      where i.UserName == userName
                                      select i).FirstOrDefault();
            var barMethodClass = new BarMethodClass()
            {   
                Date = newBarMethodClass.Date,
                Name = newBarMethodClass.Name,
                Type = newBarMethodClass.Type,
            };
            selectedInstructor.BarMethodClasses.Add(barMethodClass);
            _repo.Update(selectedInstructor);
        }

        public IList<BarMethodClassVM> ListBarClasses() // may not ever need this method
        {
            var barClasses = (from bc in _repo.Query<BarMethodClass>() select new BarMethodClassVM()
            {
                Name = bc.Name,
                Date = bc.Date,
                Type = bc.Type
            }).ToList();
            return barClasses;
        }
    }
}
