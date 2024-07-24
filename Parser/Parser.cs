namespace CalcApi.Parser
{
    public class Parser : IParser
    {

        private static readonly Dictionary<string, int> OperatorsPrecedence = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "^", 3 }
        };

        public double Evaluate(string expression)
        {
            var tokens = Tokenize(expression);
            var rpn = ConvertToRPN(tokens);
            return EvaluateRPN(rpn);
        }

        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var number = "";
            foreach (var ch in expression)
            {
                if (char.IsDigit(ch) || ch == '.')
                {
                    number += ch;
                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    if (OperatorsPrecedence.ContainsKey(ch.ToString()) || ch == '(' || ch == ')')
                    {
                        tokens.Add(ch.ToString());
                    }
                }
            }
            if (!string.IsNullOrEmpty(number))
            {
                tokens.Add(number);
            }
            return tokens;
        }

        private List<string> ConvertToRPN(List<string> tokens)
        {
            var outputQueue = new List<string>();
            var operatorStack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    outputQueue.Add(token);
                }
                else if (OperatorsPrecedence.ContainsKey(token))
                {
                    while (operatorStack.Count > 0 && OperatorsPrecedence.ContainsKey(operatorStack.Peek()) &&
                           OperatorsPrecedence[operatorStack.Peek()] >= OperatorsPrecedence[token])
                    {
                        outputQueue.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        outputQueue.Add(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
            }

            while (operatorStack.Count > 0)
            {
                outputQueue.Add(operatorStack.Pop());
            }

            return outputQueue;
        }

        private double EvaluateRPN(List<string> rpn)
        {
            var stack = new Stack<double>();

            foreach (var token in rpn)
            {
                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (OperatorsPrecedence.ContainsKey(token))
                {
                    var rightOperand = stack.Pop();
                    var leftOperand = stack.Pop();
                    stack.Push(token switch
                    {
                        "+" => leftOperand + rightOperand,
                        "-" => leftOperand - rightOperand,
                        "*" => leftOperand * rightOperand,
                        "/" => leftOperand / rightOperand,
                        "^" => Math.Pow(leftOperand, rightOperand),
                        _ => throw new ArgumentException($"Unsupported operator: {token}")
                    });
                }
            }

            return stack.Pop();
        }
    }
}