using CalcApi.Parser;
using CalcApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalcApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        private readonly ICalcService _calcService;
        private readonly IParser _parser;
        public CalcController(ICalcService calculatorService, IParser parser)
        {
            _calcService = calculatorService;
            _parser = parser;
        }

        [HttpGet("add")]
        public ActionResult<double> Add(double a, double b) => _calcService.Add(a, b);
        [HttpGet("subtract")]
        public ActionResult<double> Subtract(double a, double b) => _calcService.Subtract(a, b);

        [HttpGet("multiply")]
        public ActionResult<double> Multiply(double a, double b) => _calcService.Multiply(a, b);

        [HttpGet("divide")]
        public ActionResult<double> Divide(double a, double b)
        {
            if (b != 0)
            {

                return _calcService.Divide(a, b);
            }
            else
            {
                return BadRequest(new { error = "Division by zero is not allowed." });
            }
        }

        [HttpGet("pow")]
        public ActionResult<double> Pow(double a, double b) => _calcService.Pow(a, b);

        [HttpGet("root")]
        public ActionResult<double> Root(double a, double b)
        {
            if (a < 0)
                return BadRequest(new { error = "Cannot take root of a negative number." });
            else
                return _calcService.Root(a, b);
        }

        [HttpGet("evaluate")]
        public ActionResult<double> Evaluate(string expression)
        {
            try
            {
                return _calcService.Evaluate(expression, _parser);
            }
            catch
            {
                return BadRequest(new { error = "Invalid error" });

            }
        }
    }
}
