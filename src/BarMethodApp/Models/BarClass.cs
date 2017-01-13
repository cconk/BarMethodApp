﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarMethodApp.Models
{
    public class BarClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Instructor { get; set; }
        public string Type { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
