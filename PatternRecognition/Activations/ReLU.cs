using System;
using PatternRecognition.Interfaces;

namespace ConvolutionalNetwork
{
    namespace Activations
    {
        public class ReLU : IActivation
        {
            public double f(double x)
            {
                return Math.Max(0, x);
            }

            public double df(double x)
            {
                return x > 0 ? 1.0 : 0.0;
            }

            public string Type()
            {
                return "ReLU";
            }
        }
    }
}