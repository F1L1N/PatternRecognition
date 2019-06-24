using PatternRecognition.Interfaces;

namespace ConvolutionalNetwork
{
    namespace Activations
    {
        public class Identity : IActivation
        {
            public double f(double x)
            {
                return x;
            }
            public double df(double x)
            {
                return 1;
            }
            public string Type()
            {
                return "Identity";
            }
        }
    }
}