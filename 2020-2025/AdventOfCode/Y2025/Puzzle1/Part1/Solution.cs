namespace AdventOfCode.Y2025.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var dial = new Dial(totalRotations: 99, currentPosition: 50);
            var zeroCount = 0;

            foreach (var line in lines)
            {
                var direction = line[0];
                var rotations = int.Parse(line.Substring(1, line.Length - 1));

                dial.Rotate(rotations, direction);

                if (dial.CurrentPosition == 0)
                    zeroCount++;
            }

            Console.WriteLine(zeroCount);
        }
    }

    public class Dial
    {
        private readonly int _totalRotations;
        public int CurrentPosition { get; private set; }

        public Dial(int totalRotations, int currentPosition)
        {
            _totalRotations = totalRotations + 1;
            CurrentPosition = currentPosition;
        }

        public void Rotate(int rotations, char direction)
        {
            if (direction != 'L' && direction != 'R')
                throw new ArgumentException("Direction must be 'L' or 'R'");

            CurrentPosition = direction == 'L'
                ? (CurrentPosition - rotations + _totalRotations) % _totalRotations
                : (CurrentPosition + rotations) % _totalRotations;
        }
    }
}