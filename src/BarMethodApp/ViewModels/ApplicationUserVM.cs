using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarMethodApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarMethodApp.ViewModels
{
    public class ApplicationUserVM : IdentityUser
    {
       

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<BarMethodClassVM> BarMethodClasses { get; set; }
    }
}
