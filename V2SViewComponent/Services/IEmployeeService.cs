using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Models;

namespace V2SViewComponent.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<DepartmentHeadCount> EmployeeCountByDept();
    }
}
