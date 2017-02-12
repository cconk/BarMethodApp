using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarMethodApp.Models
{
    public class BarMethodClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public  string Type{ get; set; }
        public IList<BMCExercise> Exercises { get; set; }
        public ApplicationUser Instructor { get; set; }
        [ForeignKey("Instructor")]
        public string InstructorId { get; set; }


    }
}
