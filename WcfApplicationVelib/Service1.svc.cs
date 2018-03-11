﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

namespace WcfApplicationVelib
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Station> GetVelibOfCity(string cityName)
        {
            List<Station> stations;

            try
            {
                var city = cityName;
                WebRequest request = WebRequest.Create($"https://api.jcdecaux.com/vls/v1/stations?contract={city}&apiKey=b132038dfe4180ef1a9fc6eeac3ceec580503a57");
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                stations = JsonConvert.DeserializeObject<List<Station>>(responseFromServer);
                reader.Close();
                response.Close();
            }
            catch (WebException e)
            {
                stations = new List<Station>();
            }
            return stations;
        }
    }
}