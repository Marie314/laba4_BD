﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairdressingSalon.DAL.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Mark { get; set; }
        public DateTime DateTime { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
