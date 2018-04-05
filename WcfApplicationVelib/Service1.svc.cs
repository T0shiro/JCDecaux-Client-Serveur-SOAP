using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WcfApplicationVelib
{
    public class Service1 : IService1
    {
        // Cache to avoid multiple requests on the JCDecaux API for the same information
        private static Dictionary<string, ContractInformations> cacheForStations = new Dictionary<string, ContractInformations>();

        // JCDecaux API to perform request, stored in the ressource file api_key.txt at the root of WcfApplicationVelib
        private static string API_KEY = WcfApplicationVelib.Properties.Resources.api_key;

        public async Task<List<Station>> GetStationsOfContractNamed(string cityName)
        {
            List<Station> stations;
            // If the information searched is not in the cache OR if the information is in the cache but is outdated
            // The system makes a new request and refreshs the cache
            if (!cacheForStations.ContainsKey(cityName) || !cacheForStations[cityName].isInformationsTimeValid())
            {
                // If the information is in the cache, than it is outdated
                if (cacheForStations.ContainsKey(cityName))
                {
                    // So we delete it from the cache
                    cacheForStations.Remove(cityName);
                    System.Diagnostics.Debug.WriteLine("[LOG] Delete contract "+cityName+" from the cache because outdated");
                }
                // Make request to the JCDecaux API
                try
                {
                    string response = await makeRequest($"https://api.jcdecaux.com/vls/v1/stations?contract={cityName}&apiKey="+API_KEY);
                    System.Diagnostics.Debug.WriteLine("[LOG] Request performed for the contract " + cityName);
                    stations = JsonConvert.DeserializeObject<List<Station>>(response);
                    DateTime now = DateTime.Now;
                    cacheForStations.Add(cityName, new ContractInformations(stations.ToArray(), now));
                    System.Diagnostics.Debug.WriteLine("[LOG] Contract "+cityName+" stored in the cache at "+now);
                }
                catch (WebException e)
                {
                    stations = new List<Station>();
                }
            }
            // If the information is in the cache and not outdated, the system gets the informations on the cache
            else
            {
                System.Diagnostics.Debug.WriteLine("[LOG] Contract " + cityName + " in the cache and still valid");
                stations = cacheForStations[cityName].GetStations().ToList();
                System.Diagnostics.Debug.WriteLine("[LOG] Contract " + cityName + " loaded from the cache");
            }
            return stations;
        }

        public async Task<List<string>> GetContracts()
        {
            
            System.Diagnostics.Debug.WriteLine(await makeRequest("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + API_KEY));
            return(JArray.Parse(await makeRequest("https://api.jcdecaux.com/vls/v1/contracts?apiKey="+API_KEY)))
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

        public List<Station> GetStationsOfContractNamedSync(string cityName)
        {
            List<Station> stations;
            // If the information searched is not in the cache OR if the information is in the cache but is outdated
            // The system makes a new request and refreshs the cache
            if (!cacheForStations.ContainsKey(cityName) || !cacheForStations[cityName].isInformationsTimeValid())
            {
                // If the information is in the cache, than it is outdated
                if (cacheForStations.ContainsKey(cityName))
                {
                    // So we delete it from the cache
                    cacheForStations.Remove(cityName);
                    System.Diagnostics.Debug.WriteLine("[LOG] Delete contract " + cityName + " from the cache because outdated");
                }
                // Make request to the JCDecaux API
                try
                {
                    string response = makeRequestSync($"https://api.jcdecaux.com/vls/v1/stations?contract={cityName}&apiKey=" + API_KEY);
                    System.Diagnostics.Debug.WriteLine("[LOG] Request performed for the contract " + cityName);
                    stations = JsonConvert.DeserializeObject<List<Station>>(response);
                    DateTime now = DateTime.Now;
                    cacheForStations.Add(cityName, new ContractInformations(stations.ToArray(), now));
                    System.Diagnostics.Debug.WriteLine("[LOG] Contract " + cityName + " stored in the cache at " + now);
                }
                catch (WebException e)
                {
                    stations = new List<Station>();
                }
            }
            // If the information is in the cache and not outdated, the system gets the informations on the cache
            else
            {
                System.Diagnostics.Debug.WriteLine("[LOG] Contract " + cityName + " in the cache and still valid");
                stations = cacheForStations[cityName].GetStations().ToList();
                System.Diagnostics.Debug.WriteLine("[LOG] Contract " + cityName + " loaded from the cache");
            }
            return stations;
        }

        private string makeRequestSync(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
