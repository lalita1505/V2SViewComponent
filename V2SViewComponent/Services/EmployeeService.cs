using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using V2SViewComponent.Interfaces;
using V2SViewComponent.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace V2SViewComponent.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMapper _mapper;

        public EmployeeService(IMongoClient client, IMapper mapper)
        {
            var database = client.GetDatabase("V2");
            var collection = database.GetCollection<Employee>(nameof(Employee));

            _employees = collection;
            _mapper = mapper;
        }

        public async Task CreateAsync(Employee employee)
        {
            //var empModel = _mapper.Map<Employee, EmployeeModel>(employee);
            await _employees.InsertOneAsync(employee);
        }

        public bool IsDuplicateRecord(Employee employee)
        {
            var emp = _employees.AsQueryable<Employee>().Where(e => e.Email.ToLower() == employee.Email.ToLower()).FirstOrDefault();
            if (emp == null)
                return false;
            else
                return true;
        }


        public async Task<Employee> GetByIdAsync(string id)
        {
            return await _employees.Find<Employee>(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllAsync()
        {
            //.AsQueryable()
            //.Skip((page - 1) * pageSize)
            //.Take(pageSize)
            //.ProjectTo<Book, BookListDTO>(_mapper)
            //.ToListAsync();
            //return await _employees.AsQueryable<Employee>().ToListAsync();
            return await _employees.AsQueryable().ProjectTo<Employee, EmployeeModel>(_mapper).ToListAsync();
        }

        public IEnumerable<Employee> GetSearchRecords(string searchString)
        {
            searchString = searchString.ToLower();
            //var employees =  _employees.AsQueryable().ProjectTo<Employee, EmployeeModel>(_mapper).ToList().Where(e => e.FirstName.ToLower().Contains(searchString) || e.LastName.ToLower().Contains(searchString)
            //|| e.Designation.ToLower().Contains(searchString) || e.DOB.ToString("dd/MM/yyyy").Contains(searchString));

            var employees = _employees.AsQueryable<Employee>().ToList().Where(e => e.FirstName.ToLower().Contains(searchString) || e.LastName.ToLower().Contains(searchString) || e.Designation.ToLower().Contains(searchString) || e.DOB.ToString("dd/MM/yyyy").Contains(searchString));
            return employees;
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

    public static class MongoDbExtensions
    {
        //convert the IQueryable<T> produced by automapper back to IMongoQueryable<T>.
        public static IMongoQueryable<TDestination> ProjectTo<TSource, TDestination>(this IMongoQueryable<TSource> query, AutoMapper.IMapper autoMapper) => query.ProjectTo<TDestination>(autoMapper.ConfigurationProvider) as IMongoQueryable<TDestination>;
    }
}
