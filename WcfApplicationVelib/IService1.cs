using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfApplicationVelib
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
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
        public int bike_stands { get; set; }
        [DataMember]
        public int available_bike_stands { get; set; }
        [DataMember]
        public int available_bikes { get; set; }
    }
}
