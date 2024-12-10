namespace day7;

class Program
{
    static void Main(string[] args)
    {
        List<string> instructions = File.ReadLines("./input.txt").ToList<string>();
        Int64 calibration1 = Part1(instructions);
        Int64 calibration2 = Part2(instructions);
        Console.WriteLine($"Part 1 answer: {calibration1}");
        Console.WriteLine($"Part 2 answer: {calibration2}");
    }

    private static Int64 Part1(List<string> instructions) {
        Int64 result = 0;
        foreach (string instruction in instructions) {
            string[] splitInstruction = instruction.Split(":");
            Int64 total = Int64.Parse(splitInstruction[0].TrimEnd([':', ' ']));
            List<int> values = splitInstruction[1].TrimStart().Split(' ').Select(int.Parse).ToList();

            bool foundEquation = CheckForTotalPt1(total, 0, values, 0);
            if (foundEquation) result += total;
        }
        return result;
    }

    private static Int64 Part2(List<string> instructions) {
        Int64 result = 0;
        foreach (string instruction in instructions) {
            string[] splitInstruction = instruction.Split(":");
            Int64 total = Int64.Parse(splitInstruction[0].TrimEnd([':', ' ']));
            List<int> values = splitInstruction[1].TrimStart().Split(' ').Select(int.Parse).ToList();

            bool foundEquation = CheckForTotalPt2(total, 0, values, 0);
            if (foundEquation) result += total;
        }
        return result;
    }
    
    private static bool CheckForTotalPt1(Int64 total, Int64 runningTotal, List<int> values, int index) {
        if (total == runningTotal) {
            return true;
        } else if (index == values.Count || runningTotal > total) {
            return false;
        } else {
            return CheckForTotalPt1(total, runningTotal + values[index], values, index + 1) || CheckForTotalPt1(total, runningTotal * values[index], values, index + 1);
        }
    }

    private static bool CheckForTotalPt2(Int64 total, Int64 runningTotal, List<int> values, int index) {
        if (total == runningTotal) {
            return true;
        } else if (index == values.Count || runningTotal > total) {
            return false;
        } else {
            return CheckForTotalPt2(total, runningTotal + values[index], values, index + 1) ||
                CheckForTotalPt2(total, runningTotal * values[index], values, index + 1) ||
                CheckForTotalPt2(total, Int64.Parse(runningTotal.ToString() + values[index].ToString()), values, index + 1);
        }
    }
}
