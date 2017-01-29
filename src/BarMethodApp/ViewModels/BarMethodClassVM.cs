using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarMethodApp.Models {
	public class BarMethodClassVM {
		public string Name { get; set; }
		public DateTime Date { get; set; }
        public string Type { get; set; } // might need to be an entity
        public IList<ExerciseVM> Exercises { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
    }
}
