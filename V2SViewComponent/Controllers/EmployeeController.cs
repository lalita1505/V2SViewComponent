using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;
using V2SViewComponent.Models;

namespace V2SViewComponent.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployees();
            return View(employees);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    ViewData["CurrentFilter"] = searchString;
                    var employees = _employeeService.GetSearchRecords(searchString);
                    return View("Index", employees);
                }
                else
                {
                    ViewBag.SearchMsg = "Please enter the search string";
                    return View("Index", await _employeeService.GetEmployees());
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Create
        public IActionResult Create()
        {
            ViewBag.FormAction = "Create";
            return ViewComponent("EmployeeForm");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            ViewBag.FormAction = "Create";
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _employeeService.CreateEmployee(employee);
                    if (response.StatusCode == HttpStatusCode.Created)
                        return RedirectToAction(nameof(Index));
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                        ViewBag.Message = string.Format("{0} Already Exist", employee.Email.ToUpper());
                }
                return ViewComponent("EmployeeForm", new { employee });
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.FormAction = "Edit";
            return ViewComponent("EmployeeForm", new { id });
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Employee employee)
        {
            ViewBag.FormAction = "Edit";
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _employeeService.UpdateEmployee(employee);
                    if (response.StatusCode == HttpStatusCode.OK)
                        return RedirectToAction(nameof(Index));
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                        ViewBag.Message = string.Format("{0} Already Exist", employee.Email.ToUpper());
                }
                return ViewComponent("EmployeeForm", new { employee });
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _employeeService.GetEmployeeByID(id);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
