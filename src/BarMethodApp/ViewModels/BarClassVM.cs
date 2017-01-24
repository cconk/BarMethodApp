using BarMethodApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarMethodApp.ViewModels
{
    public class BarClassVM
    {
        // id should not be passed to front end for privacy 
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Instructor { get; set; }
        public string Type { get; set; }
        public IList<ExerciseVM> Exercises { get; set; }
    }
}
