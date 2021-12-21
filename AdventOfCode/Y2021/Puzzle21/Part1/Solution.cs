namespace AdventOfCode.Y2021.Puzzle21.Part1
{
    public class Solution : ISolution
    {
        private int _dice = 0;
        private int _diceRolls = 0;

        public void Run()
        {
            int p1Position = 9, p1Score = 0;
            int p2Position = 3, p2Score = 0;

            while (true)
            {
                var p1Result = TakeTurn(p1Score, p1Position);
                p1Position = p1Result.NewPosition;
                p1Score = p1Result.NewScore;

                if (p1Score >= 1000)
                    break;

                var p2Result = TakeTurn(p2Score, p2Position);
                p2Position = p2Result.NewPosition;
                p2Score = p2Result.NewScore;

                if (p2Score >= 1000)
                    break;
            }

            Console.WriteLine(Math.Min(p1Score, p2Score) * _diceRolls);
        }

        private (int NewScore, int NewPosition) TakeTurn(int currentScore, int currentPosition)
        {
            var roll = ThrowDice();

            for (var i = 0; i < roll; i++)
            {
                currentPosition = (currentPosition == 10) ? 1 : currentPosition + 1;
            }

            currentScore += currentPosition;

            return (currentScore, currentPosition);
        }

        private int ThrowDice()
        {
            var total = 0;

            for (var d = 0; d < 3; d++)
            {
                _dice = (_dice == 10) ? 1 : _dice + 1;

                total += _dice;
                _diceRolls++;
            }

            return total;
        }
    }
}