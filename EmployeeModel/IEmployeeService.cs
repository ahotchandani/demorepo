using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FabricTransportServiceRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace EmployeeModel
{   
    public interface IEmployeeService : IService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task AddEmployee(Employee e);
        Task<Employee> GetEmployeeById(string id);
    }
}
