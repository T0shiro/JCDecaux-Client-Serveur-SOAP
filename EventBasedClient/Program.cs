using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBasedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = 10000000;
            SubscribeServiceCallBackSink objsink = new SubscribeServiceCallBackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);
            Console.WriteLine("Which contract ?");
            string contract = Console.ReadLine();
            while (true)
            {
                ServiceReference1.EventBasedServiceClient objClient = new ServiceReference1.EventBasedServiceClient(iCntxt);
                objClient.SubscribeContractInformationsModified();
                objClient.UpdateData(contract);
                Thread.Sleep(T);
            }
        }
    }
}
