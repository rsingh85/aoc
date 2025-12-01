using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Puzzle9.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var diskMap = File.ReadAllLines(Helper.GetInputFilePath(this)).First();
            var blocks = new List<string>();

            var fileId = 0;

            for (var pointer = 0; pointer < diskMap.Length; pointer++)
            {
                var diskMapItem = int.Parse(diskMap[pointer].ToString());

                if (pointer % 2 == 0)
                    AddBlocks((fileId++).ToString(), diskMapItem, blocks);
                else
                    AddBlocks(".", diskMapItem, blocks);
            }

            var compactingComplete = false;

            while (!compactingComplete)
            {
                var fileBlock = RetrieveFileBlockFromEnd(blocks);

                PlaceFileBlockInFreeSpaceFromLeft(fileBlock, blocks);

                compactingComplete = Regex.IsMatch(string.Join(string.Empty, blocks), @"^\d+\.+$");
            }

            // Calculate checksum
            long checksum = 0;

            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];

                if (block == ".") break;

                checksum += i * long.Parse(block);
            }

            Console.WriteLine(checksum);
        }

        private void PlaceFileBlockInFreeSpaceFromLeft(string fileBlock, List<string> blocks)
        {
            var indexOfFirstFreeSpace = blocks.IndexOf(".");
            blocks[indexOfFirstFreeSpace] = fileBlock;
        }

        private static string RetrieveFileBlockFromEnd(List<string> blocks)
        {
            var retrieveIndex = -1;
            var fileBlock = string.Empty;

            for (var i = blocks.Count - 1; i >= 0; i--)
            {
                if (blocks[i] != ".")
                {
                    fileBlock = blocks[i];
                    retrieveIndex = i;
                    break;
                }
            }

            blocks[retrieveIndex] = ".";
            return fileBlock;
        }

        private static void AddBlocks(string block, int length, List<string> blocks)
        {
            for (var i = 0; i < length; i++)
                blocks.Add(block);
        }
    }
}