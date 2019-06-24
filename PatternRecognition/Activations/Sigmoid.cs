using System;
using PatternRecognition.Interfaces;

namespace ConvolutionalNetwork
{
    namespace Activations
    {
        public class Sigmoid : IActivation
        {
            public double f(double x)
            {
                return 1.0 / (1.0 + Math.Exp(-x));
            }

            public double df(double x)
            {
                double fx = f(x);
                return fx * (1.0 - fx);
            }

            public string Type()
            {
                return "Sigmoid";
            }
        }
    }
}