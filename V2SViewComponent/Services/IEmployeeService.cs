﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Models;

namespace V2SViewComponent.Interfaces
{
    public interface IEmployeeService
    {
        // Create
        Task CreateAsync(Employee employee);

        // Read
        Task<Employee> GetByIdAsync(string id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetSearchRecords(string searchString);

        // Update
        Task UpdateAsync(string id, Employee employee);

        // Delete
        Task DeleteAsync(string id);
    }
}
