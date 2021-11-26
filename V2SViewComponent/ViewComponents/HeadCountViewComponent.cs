using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;

namespace V2SViewComponent.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeService _employeeService;

        public HeadCountViewComponent(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _employeeService.EmployeeCountByDept();
            return View(result);
        }
    }
}
