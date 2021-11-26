using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2SViewComponent.Models
{
    public class EmployeeRepository
    {
        public static List<Employee> AllEmployees()
        {
            return new List<Employee>()
            {
                new Employee {Id = 1, FirstName ="John", LastName = "Doe", Salary = 100000, Department = new Department{Id = 1, Name = "Technology" } },
                new Employee {Id = 2, FirstName ="Dean", LastName = "Smith", Salary = 120000, Department = new Department{Id = 1, Name = "Technology" } },
                new Employee {Id = 3, FirstName ="Jeff", LastName = "Stricklin", Salary = 130000, Department = new Department{Id = 1, Name = "Technology" } },
                new Employee {Id = 4, FirstName ="Brad", LastName = "Johnson", Salary = 50000, Department = new Department{Id = 2, Name = "HR" } },
                new Employee {Id = 5, FirstName ="Manoj", LastName = "Tiwari", Salary = 200000, Department = new Department{Id = 2, Name = "HR" } },
                new Employee {Id = 6, FirstName ="Doug", LastName = "Johns", Salary = 125000, Department = new Department{Id = 3, Name = "Sales" } },
                new Employee {Id = 7, FirstName ="Scott", LastName = "Clark", Salary = 100000, Department = new Department{Id = 3, Name = "Sales" } },
                new Employee {Id = 9, FirstName ="Michael", LastName = "Clark", Salary = 100000, Department = new Department{Id = 4, Name = "Marketing" } },
                new Employee {Id = 9, FirstName ="Johnson", LastName = "Doe", Salary = 100000, Department = new Department{Id = 4, Name = "Marketing" } },
            };
        }
    }
}
