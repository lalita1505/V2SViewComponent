using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;
using V2SViewComponent.Models;

namespace V2SViewComponent.ViewComponents
{
    public class EmployeeFormViewComponent : ViewComponent
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeFormViewComponent(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id, Employee employee)
        {
            if (!string.IsNullOrEmpty(id) && employee == null)
            {
                employee = await _employeeService.GetEmployeeByID(id);
                return View(employee);
            }
            else if (employee != null)
            {
                return View(employee);
            }
            else
                return View();
        }
    }
}
