﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2SViewComponent.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }

        public decimal Salary { get; set; }
    }
}
