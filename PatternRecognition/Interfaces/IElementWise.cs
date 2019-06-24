using PatternRecognition.Templates;

namespace PatternRecognition.Interfaces
{
    interface IElementWise
    {
        double f(Matrix matrix, int y, int x);

        double[] df(double[] matrix);

        string Type();
    }
}
