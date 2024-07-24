using CalcApi.Parser;
using System.Data;

namespace CalcApi.Services
{
    public class CalcService : ICalcService
    {
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b) => a / b;

        public double Pow(double a, double b) => Math.Pow(a, b);
        public double Root(double a, double b) => Math.Pow(a, 1 / b);

        public double Evaluate(string expression, IParser parser) => parser.Evaluate(expression);

    }
}
