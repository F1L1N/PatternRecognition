using System;
using PatternRecognition.Interfaces;

namespace ConvolutionalNetwork
{
    namespace Activations
    {
        public class Tanh : IActivation
        {
            public double f(double x)
            {
                return Math.Tanh(x);
            }

            public double df(double x)
            {
                double fx = f(x);
                return 1.0 - fx * fx;
            }

            public string Type()
            {
                return "Tanh";
            }
        }
    }
}
