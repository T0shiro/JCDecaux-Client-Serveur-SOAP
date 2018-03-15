using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationGUI
{
    // Class representing a contract in the cache
    // The contract contains a list of stations and a timestamp to know when it is outdated
    class ContractInformations
    {
        // Number of seconds when the informations is valid in the cache (here 5 minutes)
        // When the time is passed, the contract data has to be refreshed when the user asked for it
        private static readonly int dataValidTime = 300;

        private ServiceJCDecauxReference.Station[] stations;
        private DateTime timestamp;

        public ContractInformations(ServiceJCDecauxReference.Station[] stations, DateTime timestamp)
        {
            this.stations = stations;
            this.timestamp = timestamp;
        }

        // check if the contract data stored in the cache is still valid for the user
        public bool isInformationsTimeValid()
        {
            TimeSpan t = DateTime.Now - timestamp;
            return t.TotalSeconds < dataValidTime;
        }

        public ServiceJCDecauxReference.Station[] GetStations()
        {
            return stations;
        }
    }
}
