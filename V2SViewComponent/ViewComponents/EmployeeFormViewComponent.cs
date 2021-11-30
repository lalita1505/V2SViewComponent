using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;

namespace V2SViewComponent.ViewComponents
{
    public class EmployeeFormViewComponent : ViewComponent
    {
        //private readonly IEmployeeService _employeeService;

        //public EmployeeFormViewComponent(IEmployeeService employeeService)
        //{
        //    _employeeService = employeeService;
        //}

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
