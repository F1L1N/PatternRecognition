using System;
using PatternRecognition.Interfaces;

namespace PatternRecognition.Loss
{
    public class BinaryCrossEntropy : ILoss
    {
        public double f(double y, double t)
        {
            if (y == t)
            {
                return 0;
            }
            return -(t * Math.Log(y) + (1.0 - t) * Math.Log(1.0 - y));
        }

        public double df(double y, double t)
        {
            return (y - t);
        }

        public string Type()
        {
            return "BinaryCrossEntropy";
        }
    }
}
