using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Puzzle18.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(@"Y2020\Puzzle18\Part2\Input.txt");
            long sum = 0;

            foreach (var expression in input)
            {
                sum += Evaluate(expression);
            }

            Console.WriteLine(sum);
        }

        private long Evaluate(string expression)
        {
            long result = 0;

            // remove any unnecessary characters from the expression
            var sanitisedExpression = expression.Replace(" ", string.Empty);
            sanitisedExpression = EvaluateAdditions(sanitisedExpression);

            var isNumberOnly = Regex.IsMatch(sanitisedExpression, @"^\d+$");

            if (isNumberOnly)
            {
                return long.Parse(sanitisedExpression);
            }

            // stacks of digits and operators to apply
            var digits = new Stack<long>();
            var operators = new Stack<char>();

            // read the expression backwards so that when we pop
            // we're getting the order from left to right in the expr
            for (var i = sanitisedExpression.Length - 1; i >= 0; i--)
            {
                var ch = sanitisedExpression[i];
                if (char.IsDigit(ch))
                {
                    var fullNumberAsString = string.Empty;
                    while (i >= 0 && char.IsDigit(sanitisedExpression[i]))
                    {
                        fullNumberAsString = sanitisedExpression[i] + fullNumberAsString;
                        i--;
                    }
                    i++;
                    digits.Push(long.Parse(fullNumberAsString));
                }
                else if (ch == '+' || ch == '*')
                {
                    operators.Push(ch);
                }
                else if (ch == ')')
                {
                    var innerExpression = string.Empty;
                    var bracketsStack = new Stack<char>();
                    bracketsStack.Push(ch);

                    while (i > 0)
                    {
                        i--;

                        var nextChar = sanitisedExpression[i];
                        innerExpression = nextChar + innerExpression;

                        if (nextChar == ')')
                        {
                            bracketsStack.Push(nextChar);
                        }
                        else if (nextChar == '(')
                        {
                            bracketsStack.Pop();
                        }
                        if (bracketsStack.Count == 0)
                        {
                            innerExpression = innerExpression.Substring(1, innerExpression.Length - 1);
                            break;
                        }
                    }

                    var innerExpressionResult = Evaluate(innerExpression);
                    sanitisedExpression = sanitisedExpression.Replace($"({innerExpression})", innerExpressionResult.ToString());

                    if (!sanitisedExpression.Contains(")"))
                    {
                        return Evaluate(sanitisedExpression);
                    }

                    i = sanitisedExpression.Length;
                }
            }

            while (operators.Any())
            {
                var lhs = (result == 0) ? digits.Pop() : result;
                var rhs = digits.Pop();
                var op = operators.Pop();

                switch (op)
                {
                    case '+':
                        result = lhs + rhs;
                        break;
                    case '*':
                        result = lhs * rhs;
                        break;
                }
            }

            return result;
        }

        private string EvaluateAdditions(string expression)
        {
            while (Regex.IsMatch(expression, @"\(?(\d+)\+(\d+)(\+\d+)*\)?"))
            {
                var additionMatches = Regex.Matches(expression, @"(\d+)\+(\d+)(\+\d+)*");

                foreach (Match match in additionMatches)
                {
                    var vals = match.Value.Split('+');
                    var res = vals.Select(long.Parse).Sum();

                    expression = expression.Replace(match.Value, res.ToString());
                }
            }

            return expression;
        }
    }
}