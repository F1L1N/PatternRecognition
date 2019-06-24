using PatternRecognition.Interfaces;

namespace PatternRecognition.Loss
{
    public class MSE : ILoss
    {
        public double f(double y, double t)
        {
            return (y - t) * (y - t) / 2.0;
        }
        public double df(double y, double t)
        {
            return y - t;
        }
        public string Type()
        {
            return "MSE";
        }
    }
}
