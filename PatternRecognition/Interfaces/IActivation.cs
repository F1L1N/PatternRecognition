namespace PatternRecognition.Interfaces
{
    interface IActivation
    {
        double f(double x);

        double df(double x);

        string Type();
    }
}
