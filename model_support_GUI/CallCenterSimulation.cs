using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_support_GUI
{
    public class CallCenterSimulation
    {
        private List<Operator> operators;
        private List<Request> requests;

        public CallCenterSimulation(List<Operator> operators, List<Request> requests)
        {
            this.operators = operators;
            this.requests = requests;
        }

        public void Simulate(int waitingTime)
        {
            foreach (Request request in requests)
            {
                Operator selectedOperator = operators.OrderBy(oper => oper.endTime).FirstOrDefault();
                if (selectedOperator.endTime > request.ArrivalTime) request.Delay = selectedOperator.endTime - request.ArrivalTime;
                if (request.Delay < waitingTime)
                {
                    selectedOperator.HandleCall(request);
                    selectedOperator.endTime = request.ArrivalTime + request.ProcessingTime + request.Delay;
                    if (request.IsProcessedSuccessfully) selectedOperator.successCalls++;
                    selectedOperator.calls++;
                    
                }
                else request.Delay = waitingTime;
            }
        }
    }
}
