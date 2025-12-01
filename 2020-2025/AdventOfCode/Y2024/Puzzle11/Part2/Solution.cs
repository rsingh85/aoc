namespace AdventOfCode.Y2024.Puzzle11.Part2
{
    public class Solution : ISolution
    {
        private Dictionary<long, long> _cache = [];
        public void Run()
        {
            var stones = File.ReadAllLines(Helper.GetInputFilePath(this)).First().Split(' ').Select(long.Parse).ToList();

            const int MaxBlinks = 75;

            foreach (var stone in stones)
            {
                IncrementCacheValue(stone);
            }

            for (var blinks = 0; blinks < MaxBlinks; blinks++)
            {
                var stoneKvs = _cache.Keys.Select(k => (key: k, currentCount: _cache[k])).ToList();

                foreach (var stoneKv in stoneKvs)
                {
                    DecrementCacheValue(stoneKv.key, stoneKv.currentCount);

                    if (stoneKv.key == 0)
                    {
                        IncrementCacheValue(1, stoneKv.currentCount);
                    }
                    else if (stoneKv.key.ToString().Length % 2 == 0)
                    {
                        var stoneEngraving = stoneKv.key.ToString();
                        var leftStone = long.Parse(stoneEngraving.Substring(0, stoneEngraving.Length / 2));
                        var rightStone = long.Parse(stoneEngraving.Substring(stoneEngraving.Length / 2));

                        IncrementCacheValue(leftStone, stoneKv.currentCount);
                        IncrementCacheValue(rightStone, stoneKv.currentCount);
                    }
                    else
                    {
                        IncrementCacheValue(stoneKv.key * 2024, stoneKv.currentCount);
                    }
                }
                
                //Console.WriteLine("Blink #{0}", blinks + 1);
                //Console.WriteLine(string.Join(" ", _cache.Keys.Select(k => $"{k}({_cache[k]})")));
            }

            Console.WriteLine(_cache.Values.Sum());
        }

        private void IncrementCacheValue(long key, long incrementBy = 1)
        {
            if (_cache.TryGetValue(key, out long value))
                _cache[key] = value + incrementBy;
            else
                _cache.Add(key, incrementBy);
        }

        private void DecrementCacheValue(long key, long decrementBy = 1)
        {
            if (_cache.TryGetValue(key, out long value))
            {
                if ((value - decrementBy) <= 0)
                    _cache.Remove(key);
                else
                    _cache[key] = value - decrementBy;
            }
        }
    }
}