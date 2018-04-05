using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfApplicationVelib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EventBasedService : IEventBasedService
    {
        static Action<List<string>> m_Event1 = delegate { };

        public void SubscribeContractInformationsModified()
        {
            ISubscribeServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ISubscribeServiceEvents>();
            m_Event1 += subscriber.ContractInformationsModified;
        }

        public void UpdateData(string name)
        {
            Service1 service = new Service1();
            List<Station> stations = service.GetStationsOfContractNamedSync(name);
            List<string> result = new List<string>();
            foreach(Station station in stations)
            {
                result.Add("    - "+station.name + " : available bikes " + station.available_bikes);
            }
            m_Event1(result);
        }
    }
}
