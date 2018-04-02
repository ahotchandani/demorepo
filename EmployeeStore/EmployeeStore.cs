using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using EmployeeModel;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace EmployeeStore
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    [DataContract]
    internal sealed class EmployeeStore : StatefulService, IEmployeeService
    {
        EmployeeService service;
        public EmployeeStore(StatefulServiceContext context)
            : base(context)
        {
            service = new EmployeeService(this.StateManager);
        }

        public async Task AddEmployee(Employee e)
        {
            await service.AddEmployee(e);
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            return await service.GetEmployeeById(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await service.GetEmployees();
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            //return new ServiceReplicaListener[0];
            return this.CreateServiceRemotingReplicaListeners();
        }        
    }
}
