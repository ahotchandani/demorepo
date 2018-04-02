using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModel
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [DataMember]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
