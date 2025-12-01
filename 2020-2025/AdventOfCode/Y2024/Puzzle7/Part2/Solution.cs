namespace AdventOfCode.Y2024.Puzzle7.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            long totalCalibrationResult = 0;

            foreach (var line in lines)
            {
                var split = line.Split(':');

                var expectedAnswer = long.Parse(split[0]);
                var operands = split[1].Trim().Split(' ').Select(long.Parse).ToArray();

                if (CanEquationEvaluateToExpectedAnswer(expectedAnswer, operands))
                    totalCalibrationResult += expectedAnswer;
            }

            Console.WriteLine(totalCalibrationResult);
        }

        private static bool CanEquationEvaluateToExpectedAnswer(long expectedAnswer, long[] operands)
        {
            var operatorPermutations = new List<string>();
            GenerateOperatorPermutations(operands.Length - 1, operatorPermutations);

            foreach (var operatorPermutation in operatorPermutations)
            {
                var evaluatedAnswer = operands[0];
                int operandIndex = 1, operatorIndex = 0;

                while (operandIndex < operands.Length)
                {
                    var currentOperator = operatorPermutation[operatorIndex];
                    var currentOperand = operands[operandIndex];

                    if (currentOperator == '+')
                        evaluatedAnswer = evaluatedAnswer + currentOperand;

                    if (currentOperator == '*')
                        evaluatedAnswer = evaluatedAnswer * currentOperand;

                    if (currentOperator == '|')
                        evaluatedAnswer = long.Parse($"{evaluatedAnswer}{currentOperand}");

                    operatorIndex++;
                    operandIndex++;
                }

                if (evaluatedAnswer == expectedAnswer)
                    return true;
            }

            return false;
        }

        private static void GenerateOperatorPermutations(int length, List<string> permutations, string currentPermutation = "")
        {
            if (currentPermutation.Length == length)
            {
                permutations.Add(currentPermutation);
                return;
            }

            var operators = new char[] { '+', '*', '|' };

            foreach (var op in operators)
            {
                GenerateOperatorPermutations(
                    length, 
                    permutations,
                    currentPermutation + op
                );
            }
        }
    }
}