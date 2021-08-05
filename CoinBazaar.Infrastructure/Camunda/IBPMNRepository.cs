using System.Collections.Generic;

namespace CoinBazaar.Infrastructure.Camunda
{
    public interface IBPMNRepository
    {
        void StartProcessInstance(string processInstanceName, IList<KeyValuePair<string, object>> processVariables);
    }
}
