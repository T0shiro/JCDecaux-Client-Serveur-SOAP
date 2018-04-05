using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfApplicationVelib
{
    [ServiceContract(CallbackContract = typeof(ISubscribeServiceEvents))]
    interface IEventBasedService
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeContractInformationsModified();

        [OperationContract(IsOneWay = true)]
        void UpdateData(string name);
    }
}
