namespace AdventOfCode.Y2021.Puzzle16.Part2
{
    public class Solution : ISolution
    {
        private int _versionSum = 0;
        private int _indexCursor = 0;
        private string _binary;

        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this)).First();

            _binary = ConvertHexToBinary(input);

            var packet = ParsePacket();

            Console.WriteLine((packet as OperatorPacket).Evaluate());
        }

        private string ConvertHexToBinary(string hex)
        {
            return string.Join(string.Empty, hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }

        private Packet ParsePacket()
        {
            var version = Convert.ToInt32($"{_binary[_indexCursor++]}{_binary[_indexCursor++]}{_binary[_indexCursor++]}", 2);
            var typeId = Convert.ToInt32($"{_binary[_indexCursor++]}{_binary[_indexCursor++]}{_binary[_indexCursor++]}", 2);

            _versionSum += version;

            if (typeId != 4)
            {
                return ParseOperatorPacket(version, typeId);
            }

            return ParseLiteralValuePacket(version, typeId);
        }

        private OperatorPacket ParseOperatorPacket(int version, int typeId)
        {
            var packet = new OperatorPacket();
            packet.Version = version;
            packet.TypeId = typeId;
            packet.LengthTypeId = _binary[_indexCursor++];
            packet.SubPackets = new List<Packet>();

            if (packet.LengthTypeId == '0')
            {
                var totalLengthInBits = Convert.ToInt32(_binary.Substring(_indexCursor, 15), 2);
                _indexCursor += 15;

                var targetIndexCursor = _indexCursor + totalLengthInBits;

                while (_indexCursor < targetIndexCursor)
                {
                    packet.SubPackets.Add(ParsePacket());
                }
            }

            if (packet.LengthTypeId == '1')
            {
                var numberOfSubPackets = Convert.ToInt32(_binary.Substring(_indexCursor, 11), 2);
                _indexCursor += 11;
                for (var i = 0; i < numberOfSubPackets; i++)
                {
                    packet.SubPackets.Add(ParsePacket());
                }
            }

            return packet;
        }

        private LiteralValuePacket ParseLiteralValuePacket(int version, int typeId)
        {
            var packet = new LiteralValuePacket();
            packet.Version = version;
            packet.TypeId = typeId;

            const int chunkSize = 5;
            var literalValueInBinary = string.Empty;
            var lastChunkParsed = false;

            while (!lastChunkParsed)
            {
                var currentChunk = string.Empty;

                for (var i = _indexCursor; i < _indexCursor + chunkSize; i++)
                {
                    currentChunk += _binary[i];

                    if (currentChunk[0] == '0')
                    {
                        lastChunkParsed = true;
                    }
                }

                literalValueInBinary += string.Join(string.Empty, currentChunk.Skip(1));
                _indexCursor += 5;
            }

            packet.Value = Convert.ToInt64(literalValueInBinary, 2);

            return packet;
        }
    }

    public abstract class Packet
    {
        public int Version { get; set; }
        public int TypeId { get; set; }
    }

    public class LiteralValuePacket : Packet
    {
        public long Value { get; set; }
    }

    public class OperatorPacket : Packet
    {
        public int LengthTypeId { get; set; }
        public List<Packet> SubPackets { get; set; }

        public long Evaluate()
        {
            var literals = new List<long>();

            foreach (var subpacket in SubPackets)
            {
                if (subpacket is LiteralValuePacket literalValuePacket)
                {
                    literals.Add(literalValuePacket.Value);
                }
                else if (subpacket is OperatorPacket operatorPacket)
                {
                    literals.Add(operatorPacket.Evaluate());
                }
            }

            long answer = -1;

            switch (TypeId)
            {
                case 0:
                    answer = literals.Sum();
                    break;
                case 1:
                    answer = literals.First();

                    for (var i = 1; i < literals.Count; i++)
                    {
                        answer *= literals[i];
                    }
                    break;
                case 2:
                    answer = literals.Min();
                    break;
                case 3:
                    answer = literals.Max();
                    break;
                case 5:
                    answer = literals[0] > literals[1] ? 1 : 0;
                    break;
                case 6:
                    answer = literals[0] < literals[1] ? 1 : 0;
                    break;
                case 7:
                    answer = literals[0] == literals[1] ? 1 : 0;
                    break;
            }

            return answer;
        }
    }
}