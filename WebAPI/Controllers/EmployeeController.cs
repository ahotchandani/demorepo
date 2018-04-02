using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private IEmployeeService service;

        public EmployeeController()
        {
            service = ServiceProxy.Create<IEmployeeService>(
                new Uri("fabric:/Employee/EmployeeStore"),
                new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await service.GetEmployees();
            }
            catch(Exception ex)
            {

            }

            return new List<Employee>();
        }

        [HttpPost]
        public async Task AddEmployee([FromBody]Employee e)
        {
            await service.AddEmployee(e);
        }

        ////[HttpGet]
        ////public async Task<Employee> GetEmployeeById(string id)
        ////{
        ////    return await service.GetEmployeeById(id);
        ////}
    }
}