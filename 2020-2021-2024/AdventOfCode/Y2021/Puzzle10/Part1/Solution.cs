namespace AdventOfCode.Y2021.Puzzle10.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var illegalChars = new List<char>();
            var openChars = new List<char> { '(', '[', '{', '<' };

            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                var syntaxErrorOnLine = false;

                for (var i = 0; i < line.Length && !syntaxErrorOnLine; i++)
                {
                    var character = line[i];

                    if (openChars.Contains(character))
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
                            syntaxErrorOnLine = true;
                            illegalChars.Add(character);
                        }
                    }
                }
            }

            var a = illegalChars.Count(c => c == ')') * 3;
            var b = illegalChars.Count(c => c == ']') * 57;
            var c = illegalChars.Count(c => c == '}') * 1197;
            var d = illegalChars.Count(c => c == '>') * 25137;

            Console.WriteLine(a + b + c + d);
        }
    }
}