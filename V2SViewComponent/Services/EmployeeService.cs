using MongoDB.Bson;
using MongoDB.Driver;
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
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeService(IMongoClient client)
        {
            var database = client.GetDatabase("V2");
            var collection = database.GetCollection<Employee>(nameof(Employee));

            _employees = collection;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
            return employee;
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            return await _employees.Find<Employee>(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return  await _employees.Find(e => true).ToListAsync();
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
