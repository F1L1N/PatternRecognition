using PatternRecognition.Interfaces;
using PatternRecognition.Templates;

namespace PatternRecognition.Poolings
{
    public class Average : IPooling
    {
        public double f(Matrix matrix)
        {
            return matrix.Average();
        }

        public Matrix df(Matrix matrix)
        {
            return matrix / (matrix.CountColumn * matrix.CountRow);
        }

        public string Type()
        {
            return "Average";
        }
    }
}
