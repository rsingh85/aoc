namespace AdventOfCode.Y2021.Puzzle17.Part1
{
    public class Solution : ISolution
    {
        private int _minX = 14, _maxX = 50, _minY = -267, _maxY = -225;

        public void Run()
        {
            var highestY = 0;

            for (var xv = 1; xv <= _maxX; xv++)
            {
                for (var yv = _maxY; yv <= 300; yv++)
                {
                    var result = HitsTarget(xv,yv);

                    if (result.TargetHit)
                    {
                        highestY = Math.Max(highestY, result.HighestY);
                    }
                }
            }

            Console.WriteLine(highestY);
        }

        private (bool TargetHit, int HighestY) HitsTarget(int xVelocity, int yVelocity)
        {
            int x = 0, y = 0, highestY = 0;

            while (true)
            {
                x += xVelocity;
                y += yVelocity;

                highestY = Math.Max(highestY, y);

                if (IsInTargetArea(x, y))
                {
                    return (true, highestY);
                }
                else if (MissesTargetArea(x, y))
                {
                    return (false, 0);
                }

                if (xVelocity > 0)
                {
                    xVelocity--;
                }
                else if (xVelocity < 0)
                {
                    xVelocity++;
                }

                yVelocity--;
            }
        }

        private bool IsInTargetArea(int x, int y)
        {
            return x >= _minX && x <= _maxX && y >= _minY && y <= _maxY;
        }

        private bool MissesTargetArea(int x, int y)
        {
            return x > _maxX || y < _minY;
        }
    }
}