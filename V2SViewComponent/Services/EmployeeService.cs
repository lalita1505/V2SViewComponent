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
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace V2SViewComponent.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        HttpClient httpClient;

        public EmployeeService(IMongoClient client, IMapper mapper, IConfiguration configuration)
        {
            var database = client.GetDatabase("V2");
            var collection = database.GetCollection<Employee>(nameof(Employee));

            _employees = collection;
            _mapper = mapper;
            _configuration = configuration;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("WebAPIBaseUrl"));
        }

        public async Task<HttpResponseMessage> CreateEmployee(Employee employee)
        {
            string data = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(httpClient.BaseAddress + "CreateEmployee", content);
        }

        public async Task<Employee> GetEmployeeByID(string id)
        {
            Employee employee = new Employee();
            var response = await httpClient.GetAsync(httpClient.BaseAddress + "GetEmployee/" + id);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(data);
            }
            return employee;
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            IEnumerable<EmployeeModel> employees = null;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + "GetEmployees");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeModel>>(data);
            }
            return employees;
        }

        public IEnumerable<EmployeeModel> GetSearchRecords(string searchString)
        {
            searchString = searchString.ToLower();
            var employees = _employees.AsQueryable().ProjectTo<Employee, EmployeeModel>(_mapper).ToList().Where(e => e.FirstName.ToLower().Contains(searchString) || e.LastName.ToLower().Contains(searchString) || e.Designation.ToLower().Contains(searchString) || e.DOB.ToString("dd/MM/yyyy").Contains(searchString));
            return employees;
        }

        public async Task<HttpResponseMessage> UpdateEmployee(Employee employee)
        {
            string data = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return await httpClient.PutAsync(httpClient.BaseAddress + "UpdateEmployee/" + employee.Id, content);
        }

        public async Task<HttpResponseMessage> DeleteEmployee(string id)
        {
           return await httpClient.DeleteAsync(httpClient.BaseAddress + "DeleteEmployee/" + id);
        }
    }

    public static class MongoDbExtensions
    {
        //convert the IQueryable<T> produced by automapper back to IMongoQueryable<T>.
        public static IMongoQueryable<TDestination> ProjectTo<TSource, TDestination>(this IMongoQueryable<TSource> query, AutoMapper.IMapper autoMapper) => query.ProjectTo<TDestination>(autoMapper.ConfigurationProvider) as IMongoQueryable<TDestination>;
    }
}
