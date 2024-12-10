using System.Transactions;

namespace day9;

class Program
{
    static void Main(string[] args)
    {
        string sequence = File.ReadAllText("./input.txt");
        int checksum1 = Part1(sequence);
        int checksum2 = Part2(sequence);
        Console.WriteLine($"Part 1 answer: {checksum1}");
        Console.WriteLine($"Part 2 answer: {checksum2}");
    }

    private static int Part1(string sequence) {
        int checksum = 0;
        List<int> blockLengths = [], gapLengths = [];
        for (int i = 0; i < sequence.Length; i++) {
            if (i % 2 == 0) {
                blockLengths.Add(sequence[i] - '0');
            } else {
                gapLengths.Add(sequence[i] - '0');
            }
        }
        
        int currentEndId = blockLengths.Count - 1, stringIndex = 0, endBlockCount = 0, j = 0;
        while (j < currentEndId) {
            for (int x = 0; x < blockLengths[j]; x++) {
                Console.WriteLine($"Adding to checksum: stringIndex: {stringIndex} * ID: {j}");
                checksum += stringIndex * j;
                stringIndex++;
            }

            if (j != blockLengths.Count - 1) {
                for (int y = 0; y < gapLengths[j]; y++) {
                    if (stringIndex == 26) {
                        Console.WriteLine("debug");
                    }
                    Console.WriteLine($"Adding to checksum: stringIndex: {stringIndex} * End ID: {currentEndId}");
                    checksum += stringIndex * currentEndId;
                    stringIndex++;
                    endBlockCount++;
                    if (endBlockCount == blockLengths[currentEndId]) {
                        endBlockCount = 0;
                        currentEndId--;
                    }
                }
            }
            j++;
            if (j == currentEndId) {
                while (endBlockCount < blockLengths[currentEndId]) {
                    Console.WriteLine($"Adding to checksum during cleanup: stringIndex: {stringIndex} * End ID: {currentEndId}");
                    checksum += stringIndex * currentEndId;
                    stringIndex++;
                    endBlockCount++;
                }
            }
        }
        return checksum;
    }

    private static int Part2(string sequence) {
        int checksum = 0;
        return checksum;
    }
}
