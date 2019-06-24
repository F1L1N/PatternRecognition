using System;
using PatternRecognition.Interfaces;

namespace PatternRecognition.Loss
{
    public class MultiCrossEntropy : ILoss
    {
        public double f(double y, double t)
        {
            if (y == t || t == 0)
            {
                return 0;
            }
            return -(t * Math.Log(y));
        }
        public double df(double y, double t)
        {
            return y - t;
        }
        public string Type()
        {
            return "MultiClassCrossEntropy";
        }
    }
}
