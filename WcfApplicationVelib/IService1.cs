using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfApplicationVelib
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici

        [OperationContract]
        List<Station> GetVelibOfCity(string cityName);
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }

        public override string ToString()
        {
            return $"{nameof(lat)}: {lat}, {nameof(lng)}: {lng}";
        }

        public Position(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }

    [DataContract]
    public class Station
    {
        public int Number { get; set; }
        [DataMember]
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        [DataMember]
        public string status { get; set; }
        public string contract_name { get; set; }
        public int bike_stands { get; set; }
        [DataMember]
        public int available_bike_stands { get; set; }
        [DataMember]
        public int available_bikes { get; set; }

        public Station(string name, string status, int available_bike_stands, int available_bikes)
        {
            this.name = name;
            this.status = status;
            this.available_bike_stands = available_bike_stands;
            this.available_bikes = available_bikes;
        }

        public override string ToString()
        {
            return
                $"{nameof(Number)}: {Number}, {nameof(name)}: {name}, {nameof(address)}: {address}, {nameof(position)}: {position}";
        }
    }
}
