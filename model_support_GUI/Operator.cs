using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_support_GUI
{
    public class Operator
    {
        public string Name { get; set; }
        public int calls { get; set; }
        public int successCalls { get; set; }
        public int endTime { get; set; }

        public Operator(string name)
        {
            Name = name;
            calls = 0;
            successCalls = 0;
            endTime = 0;
        }

        public void HandleCall(Request call)
        {
            double deviation = (new Random().NextDouble() - 0.5) * 0.2;
            double randomValue = new Random().NextDouble();
            if (randomValue <= call.Prob_success + deviation) call.IsProcessedSuccessfully = true;

        }
    }
}
