using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WcfApplicationVelib
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        // Cache to avoid multiple requests on the JCDecaux API for the same information
        private static Dictionary<string, ContractInformations> cacheForStations = new Dictionary<string, ContractInformations>();

        public async Task<List<Station>> GetStationsOfContractNamed(string cityName)
        {
            List<Station> stations;

            // If the information searched is not in the cache OR if the information is in the cache but is outdated
            // The system makes a new request and refreshs the cache
            if (!cacheForStations.ContainsKey(cityName) || !cacheForStations[cityName].isInformationsTimeValid())
            {
                if (!cacheForStations[cityName].isInformationsTimeValid())
                {
                    cacheForStations.Remove(cityName);
                    System.Diagnostics.Debug.WriteLine("[LOG] Delete information for the contract "+cityName+" in the cache because outdated information");
                } else
                {
                    System.Diagnostics.Debug.WriteLine("[LOG] Information for the contract "+cityName+" not stored in the cache");
                }
                try
                {
                    var city = cityName;
                    string response = await makeRequest($"https://api.jcdecaux.com/vls/v1/stations?contract={city}&apiKey=b132038dfe4180ef1a9fc6eeac3ceec580503a57");
                    stations = JsonConvert.DeserializeObject<List<Station>>(response);
                    DateTime now = DateTime.Now;
                    cacheForStations.Add(cityName, new ContractInformations(stations.ToArray(), now));
                    System.Diagnostics.Debug.WriteLine("[LOG] Contract "+cityName+" stored in cache at "+now);
                    System.Diagnostics.Debug.WriteLine("[LOG] Cache size " + cacheForStations.LongCount() +" "+cacheForStations.GetHashCode());
                }
                catch (WebException e)
                {
                    stations = new List<Station>();
                }
            }
            // If the information is in the cache and not outdated, the system gets the informations on the cache
            else
            {
                System.Diagnostics.Debug.WriteLine("[LOG] Contract " + cityName + " not in cache. Request is performed");
                stations = cacheForStations[cityName].GetStations().ToList();
            }
            return stations;
        }

        public async Task<List<string>> GetContracts()
        {
            return(JArray.Parse(await makeRequest("https://api.jcdecaux.com/vls/v1/contracts?apiKey=b132038dfe4180ef1a9fc6eeac3ceec580503a57")))
                .Children()
                .Select(child => (string)child["name"])
                .ToList();
        }

        private async Task<string> makeRequest(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = await request.GetResponseAsync();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
