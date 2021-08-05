using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.Camunda
{
    public class CamundaRepository : IBPMNRepository
    {
        private readonly CamundaClient _camundaClient;
        public CamundaRepository(string engineUrl)
        {
            _camundaClient = CamundaClient.Create(engineUrl);
        }
        public void StartProcessInstance(string processInstanceName, IList<KeyValuePair<string, object>> processVariables)
        {
            var processVariablesDict = new Dictionary<string, VariableValue>();

            foreach (var variable in processVariables)
            {
                processVariablesDict.Add(variable.Key, VariableValue.FromObject(variable.Value.ToString()));
            }

            var procIns = _camundaClient.ProcessDefinitions
                   .ByKey(processInstanceName)
                   .StartProcessInstance(new StartProcessInstance()
                   {
                       BusinessKey = Guid.NewGuid().ToString(),
                       CaseInstanceId = Guid.NewGuid().ToString(),
                       Variables = processVariablesDict
                   }).GetAwaiter().GetResult();
        }
    }
}
