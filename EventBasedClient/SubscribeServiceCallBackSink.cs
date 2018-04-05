using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBasedClient.ServiceReference1;

namespace EventBasedClient
{
    class SubscribeServiceCallBackSink : ServiceReference1.IEventBasedServiceCallback
    {


        public void ContractInformationsModified(string[] stations)
        {
            foreach (string station in stations)
            {
                Console.WriteLine(station);
            }
        }

        public void UpdatedData(string name)
        {
            //foreach (string station in stations)
            //{
            //    Console.WriteLine(station);
            //}
        }
    }
}
