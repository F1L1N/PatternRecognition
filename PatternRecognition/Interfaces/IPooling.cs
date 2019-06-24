using PatternRecognition.Templates;

namespace PatternRecognition.Interfaces
{
    interface IPooling
    {
        double f(Matrix matrix);

        Matrix df(Matrix matrix);

        string Type();
    }
}
