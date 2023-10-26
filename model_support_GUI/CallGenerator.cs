using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_support_GUI
{
    internal class CallGenerator
    {
        private Random random = new Random();
        private Dictionary<int, Tuple<int, double>> requestType = new Dictionary<int, Tuple<int, double>>();
        private int count;
        private int handingTime;
       
        public List<Request> requests = new List<Request>();
        public CallGenerator(int count, int handingTime)
        {
            this.count = count;
            this.handingTime = handingTime;
        }
        private void CreateRequestType()
        {
            double proc = 1.0 / count;
            for (int i = 1; i <= count; i++) requestType.Add(i, new Tuple<int, double>(handingTime * i, proc * i));
        }
        public List<Request> GenerateCall(int totalCalls, double callRate)
        {
            int callTime = 0;
            List<Request> requests = new List<Request>();
            CreateRequestType();
            for (int i = 0; i < totalCalls; i++)
            {
                callTime += GetPoisson(callRate); // Генерация времени между звонками
                int randomRequest = random.Next(1, count + 1);
                int proccesing = requestType[randomRequest].Item1;
                double success = requestType[randomRequest].Item2;
                Request r = new Request(callTime, proccesing, randomRequest, success);
                requests.Add(r);
            }
            return requests;
        }

        private int GetPoisson(double lambda)
        {
            double L = Math.Exp(-lambda);
            double p = 1.0;
            int k = 0;

            do
            {
                k++;
                p *= random.NextDouble();
            } while (p > L);

            return k - 1;
        }
    }
}
