using CalcApi.Parser;

namespace CalcApi.Services
{
    public interface ICalcService
    {

        double Add(double a, double b);
        double Subtract(double a, double b);
        double Multiply(double a, double b);
        double Divide(double a, double b);
        double Pow(double a, double b);
        double Root(double a, double b);
        double Evaluate(string expression, IParser parser);
    }
}
