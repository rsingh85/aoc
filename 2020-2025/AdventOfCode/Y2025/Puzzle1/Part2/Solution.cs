namespace AdventOfCode.Y2025.Puzzle1.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var dial = new Dial(totalRotations: 99, currentPosition: 50);
            
            foreach (var line in lines)
            {
                var direction = line[0];
                var rotations = int.Parse(line.Substring(1, line.Length - 1));

                dial.Rotate(rotations, direction);
            }

            Console.WriteLine(dial.ZeroCounts);
        }
    }

    public class Dial(int totalRotations, int currentPosition)
    {
        private readonly int _totalRotations = totalRotations + 1;
        public int CurrentPosition { get; private set; } = currentPosition;
        public int ZeroCounts { get; private set; } = 0;

        public void Rotate(int rotations, char direction)
        {
            if (direction != 'L' && direction != 'R')
                throw new ArgumentException("Direction must be 'L' or 'R'");

            for (var i = 0; i < rotations; i++)
            {
                CurrentPosition = direction == 'L' ? CurrentPosition - 1 : CurrentPosition + 1;
                    
                if (direction == 'L' && CurrentPosition < 0)
                    CurrentPosition = _totalRotations - 1;

                if (direction == 'R' && CurrentPosition >= _totalRotations)
                    CurrentPosition = 0;

                if (CurrentPosition == 0)
                    ZeroCounts++;
            }
        }
    }
}