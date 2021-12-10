using System.Text;

namespace AdventOfCode.Y2021.Puzzle10.Part2
{
    public class Solution : ISolution
    {
        private List<char> _openChars = new List<char> { '(', '[', '{', '<' };

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var completionStrings = GetCompletionLines(lines.Where(l => !HasSyntaxError(l)));
            var scores = new List<long>();

            foreach (var completionString in completionStrings)
            {
                long score = 0;

                foreach (var character in completionString)
                {
                    score *= 5;

                    switch (character)
                    {
                        case ')': score += 1; break;
                        case ']': score += 2; break;
                        case '}': score += 3; break;
                        case '>': score += 4; break;
                    }
                }

                scores.Add(score);
            }

            scores.Sort();

            Console.WriteLine(scores[scores.Count() / 2]);
        }

        private bool HasSyntaxError(string line)
        {
            var stack = new Stack<char>();
            var syntaxError = false;

            for (var i = 0; i < line.Length && !syntaxError; i++)
            {
                var character = line[i];

                if (_openChars.Contains(character))
                {
                    stack.Push(character);
                }
                else
                {
                    var top = stack.Pop();

                    if (top == '(' && character != ')' ||
                        top == '[' && character != ']' ||
                        top == '{' && character != '}' ||
                        top == '<' && character != '>')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private IEnumerable<string> GetCompletionLines(IEnumerable<string> incompleteLines)
        {
            var stack = new Stack<char>();

            var reverseOpenCharMap = new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            foreach (var line in incompleteLines)
            {
                foreach (var character in line)
                {
                    if (_openChars.Contains(character))
                    {
                        stack.Push(character);
                    }
                    else
                    {
                        stack.Pop();
                    }
                }

                var completionString = new StringBuilder();

                while (stack.Count > 0)
                {
                    completionString.Append(reverseOpenCharMap[stack.Pop()]);
                }

                yield return completionString.ToString();
            }
        }
    }
}