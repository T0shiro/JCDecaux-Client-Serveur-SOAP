using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfApplicationVelib
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Task<List<Station>> GetStationsOfContractNamed(string cityName);

        [OperationContract]
        Task<List<string>> GetContracts();
    }

    [DataContract]
    public class Station
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public int available_bike_stands { get; set; }
        [DataMember]
        public int available_bikes { get; set; }
    }
}
