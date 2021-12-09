using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;
using V2SViewComponent.Models;

namespace V2SViewComponent.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeService(IMongoClient client)
        {
            var database = client.GetDatabase("V2");
            var collection = database.GetCollection<Employee>(nameof(Employee));

            _employees = collection;
        }

        public async Task CreateAsync(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
        }

        public bool IsDuplicateRecord(Employee employee)
        {
            var emp = _employees.AsQueryable<Employee>().Where(e => e.FirstName.ToLower() == employee.FirstName.ToLower()).FirstOrDefault();
            if (emp == null)
                return false;
            else
                return true;
        }


        public async Task<Employee> GetByIdAsync(string id)
        {
            return await _employees.Find<Employee>(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employees.AsQueryable<Employee>().ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetSearchRecords(string searchString)
        {
            searchString = searchString.ToLower();
            var employees = _employees.AsQueryable<Employee>().Where(e => e.FirstName.ToLower().Contains(searchString) || e.LastName.ToLower().Contains(searchString)
                                         || e.Designation.ToLower().Contains(searchString));
            return await employees.ToListAsync();
        }

        public async Task UpdateAsync(string id, Employee employee)
        {
            await _employees.ReplaceOneAsync(e => e.Id == id, employee);
        }

        public async Task DeleteAsync(string id)
        {
            await _employees.DeleteOneAsync(s => s.Id == id);
        }
    }
}
