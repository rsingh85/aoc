namespace AdventOfCode.Y2021.Puzzle17.Part2
{
    public class Solution : ISolution
    {
        private int _minX = 14, _maxX = 50, _minY = -267, _maxY = -225;

        public void Run()
        {
            var set = new HashSet<string>();

            for (var xv = 1; xv <= _maxX; xv++)
            {
                for (var yv = -300; yv <= 300; yv++)
                {
                    if (HitsTarget(xv, yv))
                    {
                        set.Add($"{xv},{yv}");
                    }
                }
            }

            Console.WriteLine(set.Count());
        }

        private bool HitsTarget(int xVelocity, int yVelocity)
        {
            int x = 0, y = 0;

            while (true)
            {
                x += xVelocity;
                y += yVelocity;

                if (IsInTargetArea(x, y))
                {
                    return true;
                }
                
                if (MissesTargetArea(x, y))
                {
                    return false;
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