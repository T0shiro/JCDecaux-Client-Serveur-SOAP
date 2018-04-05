using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfApplicationVelib
{
    interface ISubscribeServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void ContractInformationsModified(List<string> stations);

        [OperationContract(IsOneWay = true)]
        void UpdatedData(string name);
    }
}
