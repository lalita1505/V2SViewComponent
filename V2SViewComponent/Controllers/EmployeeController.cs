using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var employees = await _employeeService.GetAllAsync();
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
                    return View("Index", await _employeeService.GetAllAsync());
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            ViewBag.FormAction = "Create";
            try
            {
                if (ModelState.IsValid)
                {
                    var isDupRecord = _employeeService.IsDuplicateRecord(employee);
                    if (!isDupRecord)
                    {
                        await _employeeService.CreateAsync(employee);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        ViewBag.Message = string.Format("{0} Already Exist", employee.Email.ToUpper());
                }
                return View(employee);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.FormAction = "Edit";
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Employee employee)
        {
            ViewBag.FormAction = "Edit";
            var queriedEmployee = await _employeeService.GetByIdAsync(id);
            if (queriedEmployee == null)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var isDupRecord = _employeeService.IsDuplicateRecord(employee) && (queriedEmployee.Email != employee.Email);
                    if (!isDupRecord)
                    {
                        await _employeeService.UpdateAsync(id, employee);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        ViewBag.Message = string.Format("{0} Already Exist", employee.Email.ToUpper());

                    //await _employeeService.UpdateAsync(id, employee);
                    //return RedirectToAction(nameof(Index));
                }

                return View(employee);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                await _employeeService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
