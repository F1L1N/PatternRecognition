namespace PatternRecognition.Interfaces
{
    interface ILoss
    {
        double f(double y, double t);

        double df(double y, double t);

        string Type();
    }
}
