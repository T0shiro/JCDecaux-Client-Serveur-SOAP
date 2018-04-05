using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EventBasedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SubscribeServiceCallBackSink objsink = new SubscribeServiceCallBackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);

            ServiceReference1.EventBasedServiceClient objClient = new ServiceReference1.EventBasedServiceClient(iCntxt);
            objClient.SubscribeContractInformationsModified();
            Console.WriteLine("Test");
            objClient.UpdateData("Toulouse");
            Console.WriteLine("Test");
            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }
    }
}
