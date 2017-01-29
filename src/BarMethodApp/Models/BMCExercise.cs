using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarMethodApp.Models
{
    public class BMCExercise
    {
        public int id { get; set; }
		[ForeignKey("BMClass")]
		public int BMCId { get; set; }
		public BarMethodClass BMClass { get; set; }
		[ForeignKey("Exercise")]
		public int ExerciseId { get; set; }
		public Exercise Exercise { get; set; }
	}
}
