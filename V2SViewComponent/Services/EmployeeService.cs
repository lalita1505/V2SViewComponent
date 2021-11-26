using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;
using V2SViewComponent.Models;

namespace V2SViewComponent.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<DepartmentHeadCount> EmployeeCountByDept()
        {
            return EmployeeRepository.AllEmployees().GroupBy(e => e.Department.Name)
                .Select(g => new DepartmentHeadCount { DepartmentName = g.Key, Count = g.Count() });

        }

    }
}
