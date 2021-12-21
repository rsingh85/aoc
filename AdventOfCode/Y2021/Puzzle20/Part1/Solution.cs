namespace AdventOfCode.Y2021.Puzzle20.Part1
{
    public class Solution : ISolution
    {
        private string? _indexedImageEnhancementValues;

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            _indexedImageEnhancementValues = lines.First();
            var inputImageLines = lines.Skip(2).TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToList();

            var outputImageLines = EnhanceImage(inputImageLines);
            outputImageLines = EnhanceImage(outputImageLines);

            var lightPixelCount = 0;

            foreach (var line in outputImageLines)
            {
                lightPixelCount += line.Count(c => c == '#');
            }

            Console.WriteLine(lightPixelCount);
        }

        private List<string> EnhanceImage(List<string> inputImageLines)
        {
            var outputImageLines = new List<string>();
            var boundOffset = 2;

            for (var r = -boundOffset; r < inputImageLines.Count + boundOffset; r++)
            {
                var outputImageLine = string.Empty;

                for (var c = -boundOffset; c < inputImageLines.First().Length + boundOffset; c++)
                {
                    var binary = GetBinaryForPixel(inputImageLines, r, c);
                    var decimalIndex = Convert.ToInt32(binary, 2);
                    outputImageLine += _indexedImageEnhancementValues[decimalIndex];
                }

                outputImageLines.Add(outputImageLine);
            }

            //Console.WriteLine(string.Join(Environment.NewLine, outputImageLines));
            //Console.WriteLine();

            return outputImageLines;
        }

        private string GetBinaryForPixel(List<string> inputImageLines, int r, int c)
        {
            bool topLeft = false,
                top = false,
                topRight = false,
                left = false,
                middle = false,
                right = false,
                bottomLeft = false,
                bottom = false,
                bottomRight = false;

            var maxRows = inputImageLines.Count;
            var maxCols = inputImageLines.First().Length;

            if (r - 1 >= 0 && r - 1 < maxRows && c - 1 >= 0 && c - 1 < maxCols)
                topLeft = inputImageLines[r - 1][c - 1] == '#';

            if (r - 1 >= 0 && r - 1 < maxRows && c >= 0 && c < maxCols)
                top = inputImageLines[r - 1][c] == '#';

            if (r - 1 >= 0 && r - 1 < maxRows && c + 1 >= 0 && c + 1 < maxCols)
                topRight = inputImageLines[r - 1][c + 1] == '#';

            if (r >= 0 && r < maxRows && c - 1 >= 0 && c - 1 < maxCols)
                left = inputImageLines[r][c - 1] == '#';

            if (r >= 0 && r < maxRows && c >= 0 && c < maxCols)
                middle = inputImageLines[r][c] == '#';

            if (r >= 0 && r < maxRows && c + 1 >= 0 && c + 1 < maxCols)
                right = inputImageLines[r][c + 1] == '#';

            if (r + 1 >= 0 && r + 1 < maxRows && c - 1 >= 0 && c - 1 < maxCols)
                bottomLeft = inputImageLines[r + 1][c - 1] == '#';

            if (r + 1 >= 0 && r + 1 < maxRows && c >= 0 && c < maxCols)
                bottom = inputImageLines[r + 1][c] == '#';

            if (r + 1 >= 0 && r + 1 < maxRows && c + 1 >= 0 && c + 1 < maxCols)
                bottomRight = inputImageLines[r + 1][c + 1] == '#';

            var binary = string.Empty;
            binary += topLeft ? "1" : "0";
            binary += top ? "1" : "0";
            binary += topRight ? "1" : "0";
            binary += left ? "1" : "0";
            binary += middle ? "1" : "0";
            binary += right ? "1" : "0";
            binary += bottomLeft ? "1" : "0";
            binary += bottom ? "1" : "0";
            binary += bottomRight ? "1" : "0";

            return binary;
        }
    }
}