using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using V2SViewComponent.Models;

namespace V2SViewComponent.Interfaces
{
    public interface IEmployeeService
    {
        // Create
        Task<HttpResponseMessage> CreateEmployee(Employee employee);

        // Read
        Task<Employee> GetEmployeeByID(string id);
        Task<IEnumerable<EmployeeModel>> GetEmployees();

        IEnumerable<EmployeeModel> GetSearchRecords(string searchString);

        // Update
        Task<HttpResponseMessage> UpdateEmployee(Employee employee);

        // Delete
        Task<HttpResponseMessage> DeleteEmployee(string id);
    }
}
