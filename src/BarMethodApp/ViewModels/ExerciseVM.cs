using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarMethodApp.Models {
	public class ExerciseVM {
		public string Name { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
        public int Order { get; set; }
	}
}
