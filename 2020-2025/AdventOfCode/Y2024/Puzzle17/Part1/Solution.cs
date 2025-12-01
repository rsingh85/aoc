namespace AdventOfCode.Y2024.Puzzle17.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            (int a, int b, int c) registers = (
                int.Parse(lines[0].Split(':')[1].Trim()),
                int.Parse(lines[1].Split(':')[1].Trim()),
                int.Parse(lines[2].Split(':')[1].Trim())
            );

            var program = lines[4].Split(':')[1].Trim().Split(',').Select(int.Parse).ToArray();
            var pointer = 0;

            Console.Write("Output: ");

            while (true)
            {
                if (pointer >= program.Length) break;

                var nextPointer = Execute(program[pointer], program[pointer + 1], ref registers);

                if (nextPointer == -1)
                    pointer += 2;
                else
                    pointer = nextPointer;
            }

            Console.WriteLine();
            Console.WriteLine("Registers: A={0}, B={1}, C={2}", registers.a, registers.b, registers.c);
        }

        private static int Execute(int opcode, int operand, ref (int a, int b, int c) registers)
        {
            switch (opcode)
            {
                case 0: // adv
                    registers.a = registers.a / (int) Math.Pow(2, GetComboOperand(operand, ref registers));
                    break;
                case 1: // bxl
                    registers.b = registers.b ^ operand;
                    break;
                case 2: // bst
                    registers.b = GetComboOperand(operand, ref registers) % 8;
                    break;
                case 3: // jnz
                    if (registers.a == 0) break;
                    return operand;
                case 4: // bxc
                    registers.b = registers.b ^ registers.c;
                    break;
                case 5: // out
                    Console.Write("{0},", GetComboOperand(operand, ref registers) % 8);
                    break;
                case 6: // bdv
                    registers.b = registers.a / (int) Math.Pow(2, GetComboOperand(operand, ref registers));
                    break;
                case 7: // cdv
                    registers.c = registers.a / (int) Math.Pow(2, GetComboOperand(operand, ref registers));
                    break;
            }

            return -1;
        }

        private static int GetComboOperand(int operand, ref (int a, int b, int c) registers) => 
            operand switch
            {
                int n when (n >= 0 && n <= 3) => n,
                4 => registers.a,
                5 => registers.b,
                6 => registers.c,
                _ => throw new Exception("Invalid combo operand"),
            };
    }
}