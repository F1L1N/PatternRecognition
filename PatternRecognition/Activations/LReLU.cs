using System;
using PatternRecognition.Interfaces;

namespace ConvolutionalNetwork
{
    namespace Activations
    {
        public class LReLU : IActivation
        {
            const double alpha = 0.0001;

            public double f(double x)
            {
                return Math.Max(alpha * x, x);
            }

            public double df(double x)
            {
                return x > 0 ? 1.0 : alpha;
            }

            public string Type()
            {
                return "LReLU";
            }
        }
    }
}
