using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Puzzle9.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var diskMap = File.ReadAllLines(Helper.GetInputFilePath(this)).First();
            var blocks = new List<Block>();

            long fileId = 0;

            for (var pointer = 0; pointer < diskMap.Length; pointer++)
            {
                var diskMapItem = int.Parse(diskMap[pointer].ToString());

                if (pointer % 2 == 0)
                {
                    AddBlocks(fileId, diskMapItem, blocks);
                    fileId++;
                }
                else
                    AddBlocks(null, diskMapItem, blocks);
            }

            var compactingComplete = false;

            while (!compactingComplete)
            {
                var nextFileBlocksToMove = GetNextFileBlocksToMove(blocks);

                if (nextFileBlocksToMove != null)
                {
                    var startIndexOfAvailableSpace = GetStartIndexOfAdjacentNFreeBlocks(blocks, nextFileBlocksToMove.Count);

                    if (startIndexOfAvailableSpace > -1 
                            && startIndexOfAvailableSpace < blocks.FindIndex(b => b.FileId == nextFileBlocksToMove.First().FileId))
                    {
                        var nextFileToMoveIndex = blocks.FindLastIndex(b => b.FileId == nextFileBlocksToMove.First().FileId);

                        while (nextFileToMoveIndex > -1)
                        {
                            blocks[nextFileToMoveIndex] = new Block { FileId = null };
                            nextFileToMoveIndex = blocks.FindLastIndex(b => b.FileId == nextFileBlocksToMove.First().FileId);
                        }

                        for (var i = startIndexOfAvailableSpace; i < (startIndexOfAvailableSpace + nextFileBlocksToMove.Count); i++)
                        {
                            blocks[i] = nextFileBlocksToMove[0];
                        }
                    }
                }
                else compactingComplete = true;
            }

            long checksum = 0;

            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];

                if (block.IsFree || !block.IsFile) continue;

                checksum += i * block.FileId!.Value;
            }

            Console.WriteLine(checksum);
        }

        private List<long> processedFileIds = new List<long>();

        private List<Block> GetNextFileBlocksToMove(List<Block> blocks)
        {
            for (var i = blocks.Count - 1; i >= 0; i--)
            {
                var block = blocks[i];

                if (block.IsFree || (block.IsFile && block.FileId.HasValue && processedFileIds.Contains(block.FileId.Value)))
                    continue;

                processedFileIds.Add(block.FileId!.Value);

                //var nextFileBlocks = Regex.Match(string.Join(string.Empty, blocks), $"{block}+").Value.ToArray().Select(c => c.ToString()).ToList();
                var nextFileBlocks = blocks.Where(b => b.IsFile && b.FileId.HasValue && b.FileId == block.FileId.Value).ToList();
                return nextFileBlocks;
            }

            return null;
        }

        private static void Print(List<Block> blocks)
        {
            Console.WriteLine(string.Join(string.Empty, blocks));
        }

        private static void AddBlocks(long? fileId, int length, List<Block> blocks)
        {
            for (var i = 0; i < length; i++)
                blocks.Add(new Block
                {
                    FileId = fileId
                });
        }

        public int GetStartIndexOfAdjacentNFreeBlocks(List<Block> blocks, int n)
        {
            var tempList = new List<Block>();
            var startIndex = -1;

            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];

                if (block.IsFree)
                {
                    if (startIndex == -1)
                        startIndex = i;

                    tempList.Add(block);

                    if (tempList.Count == n)
                        return startIndex;
                }
                else
                {
                    tempList.Clear();
                    startIndex = -1;
                }
            }

            return -1;
        }
    }

    public class Block
    {
        public bool IsFree => !IsFile;
        public bool IsFile => FileId.HasValue;
        public long? FileId { get; set; }

        public override string ToString()
        {
            return IsFree ? "." : $"{FileId}";
        }
    }
}