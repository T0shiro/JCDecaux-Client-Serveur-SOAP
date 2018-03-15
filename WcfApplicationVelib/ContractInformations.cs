using System;
using System.Collections.Generic;
namespace WcfApplicationVelib
{
    internal class ContractInformations
    {
        // Number of seconds when the informations is valid in the cache
        // When the time is passed, the contract data has to be refreshed when the user asked for it
        // Stored in the ressource file cache_refresh_time.txt at the root of WcfApplicationVelib
        private static readonly int DATA_VALID_TIME = Int32.Parse(WcfApplicationVelib.Properties.Resources.cache_refresh_time);

        private Station[] stations;
        private DateTime timestamp;

        public ContractInformations(Station[] stations, DateTime timestamp)
        {
            this.stations = stations;
            this.timestamp = timestamp;
        }

        // check if the contract data stored in the cache is still valid for the user
        public bool isInformationsTimeValid()
        {
            TimeSpan t = DateTime.Now - timestamp;
            return t.TotalSeconds < DATA_VALID_TIME;
        }

        public Station[] GetStations()
        {
            return stations;
        }
    }
}