using EmployeeModel;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeStore
{
    class EmployeeService : IEmployeeService
    {
        private IReliableStateManager stateMgr;
        public EmployeeService(IReliableStateManager mgr)
        {
            stateMgr = mgr;
        }
        public async Task AddEmployee(Employee e)
        {
            var employees = await stateMgr.GetOrAddAsync<IReliableDictionary<string, Employee>>("Employees");

            using (var tx = stateMgr.CreateTransaction())
            {
                await employees.AddOrUpdateAsync(tx, e.Id, e, (id, emp) => e);
                await tx.CommitAsync();
            }
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            var employees = await stateMgr.GetOrAddAsync<IReliableDictionary<string, Employee>>("Employees");
            Employee emp = null;
            using (var tx = stateMgr.CreateTransaction())
            {
              var res = await employees.TryGetValueAsync(tx, id);

                if (res.HasValue)
                {
                    emp = res.Value;
                }                
            }

            return emp;           
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await stateMgr.GetOrAddAsync<IReliableDictionary<string, Employee>>("Employees");
            List<Employee> fetchedEmployees = new List<Employee>();
            using (var tx = stateMgr.CreateTransaction())
            {
                var allemp = await employees.CreateEnumerableAsync(tx, EnumerationMode.Unordered);

                using (var enumeration = allemp.GetAsyncEnumerator())
                {
                    while(await enumeration.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<string, Employee> currentValue = enumeration.Current;
                        fetchedEmployees.Add(currentValue.Value);
                    }
                }

            }

            return fetchedEmployees;
        }
    }
}
