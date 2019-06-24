using System;
using PatternRecognition.Interfaces;

namespace ConvolutionalNetwork
{
    namespace Activations
    {
        public class PReLU : IActivation
        {
            public double f(double x)
            {
                return 0.0;
            }

            public double df(double x)
            {
                return 0.0;
            }

            public string Type()
            {
                return "PReLU";
            }
        }
    }
}
