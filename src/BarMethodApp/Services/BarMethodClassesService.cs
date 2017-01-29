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
            var selectedInstructorBarClasses = _repo.Query<ApplicationUser>().Include(i => i.BarMethodClasses).Where(i => i.UserName == userName).Select(i => new BarMethodClassVM() { }).ToList();


            //from bc in _repo.Query<BarMethodClass>()
            //join u in _repo.Query<ApplicationUser>() on bc.ApplicationUserId equals u.Id
            //select new BarMethodClassVM()
            //{
            //    Name = bc.Name
            //};


            //

            //// based on user input from front end return a list of barClasses for selected instructor
            //var selectedInstructor = (from i in _repo.Query<ApplicationUser>()
            //                          where i.UserName == userName
            //                          select new ApplicationUserVM()
            //                          {
            //                              UserName = i.UserName,

            //                          }).FirstOrDefault();
            //var barClasses = (from bc in selectedInstructor.BarMethodClasses select bc).ToList();
            return selectedInstructorBarClasses;
        }

        public void AddNewBarMethodClass(BarMethodClassVM newBarMethodClass)
        {

            var barMethodClass = new BarMethodClass()
            {
                Date = newBarMethodClass.Date,
                Name = newBarMethodClass.Name,
                Type = newBarMethodClass.Type,
            };
            _repo.Add(barMethodClass);
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
