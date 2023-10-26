using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_support_GUI
{

    public class ErlangB
    {
        public static double CalculateBlockingProbability(double A, int N)
        {
            double numerator = Math.Pow(A, N) / Factorial(N);
            double denominator = 0;
            for (int n = 0; n <= N; n++)
            {
                denominator += Math.Pow(A, n) / Factorial(n);
            }
            return numerator / denominator;
        }

        public static double Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            double result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public static int CalculateRequiredOperators(double A, double targetBlockingProbability)
        {
            int operators = 1;
            while (true)
            {
                double blockingProbability = CalculateBlockingProbability(A, operators);
                if (blockingProbability <= targetBlockingProbability)
                {
                    return operators;
                }
                operators++;
            }
        }

    }
}
