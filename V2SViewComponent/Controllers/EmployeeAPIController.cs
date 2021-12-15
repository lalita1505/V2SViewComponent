using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;
using V2SViewComponent.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace V2SViewComponent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeAPIController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeAPIController>
        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                if (employees.Count() > 0)
                {
                    return Ok(employees);
                }
            }
            catch
            {
            }
            return NotFound();
        }

        // GET api/<EmployeeAPIController>/5
        [HttpGet("GetEmployee/{id}")]
        public async Task<ActionResult> GetEmployeeByID(string id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee != null)
                    return Ok(employee);
            }
            catch
            {
            }
            return NotFound();
        }

        // POST api/<EmployeeAPIController>
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            try
            {
                var isDupRecord = _employeeService.IsDuplicateRecord(employee);
                if (!isDupRecord)
                {
                    await _employeeService.CreateAsync(employee);
                    return Ok(employee);
                }
                else
                    return BadRequest(string.Format("{0} Already Exist", employee.Email.ToUpper()));
            }
            catch
            {
            }
            return NoContent();
        }

        // PUT api/<EmployeeAPIController>/5
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, Employee employee)
        {
            var queriedEmployee = await _employeeService.GetByIdAsync(id);
            if (queriedEmployee == null)
            {
                return NotFound();
            }

            try
            {
                var isDupRecord = _employeeService.IsDuplicateRecord(employee) && (queriedEmployee.Email != employee.Email);
                if (!isDupRecord)
                {
                    await _employeeService.UpdateAsync(id, employee);
                    return Ok(employee);
                }
                else
                    return BadRequest(string.Format("{0} Already Exist", employee.Email.ToUpper()));
            }
            catch
            {
            }
            return NoContent();
        }

        // DELETE api/<EmployeeAPIController>/5
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                await _employeeService.DeleteAsync(id);
            }
            catch
            {
            }
            return NoContent();
        }
    }
}
