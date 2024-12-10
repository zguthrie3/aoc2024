// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        int multipliedSum = Part1();
        int doDontSum = Part2();
        Console.WriteLine($"Part 1 answer: {multipliedSum}");
        Console.WriteLine($"Part 2 answer: {doDontSum}");
    }

    private static int Part1() {
        string memory = File.ReadAllText("./input/input.txt");
        string mulPattern = @"mul\(\d+,\d+\)";
        int sum = 0;
        foreach (Match match in Regex.Matches(memory, mulPattern)) {
            MatchCollection digits = Regex.Matches(match.Value, @"\d+");
            sum += int.Parse(digits[0].Value) * int.Parse(digits[1].Value);
        }
        return sum;
    }

    private static int Part2() {
        string memory = File.ReadAllText("./input/input.txt");
        string superPattern = @"(don't\(\)|do\(\)|mul\(\d+,\d+\))";

        Match match = Regex.Match(memory, superPattern);
        bool isEnabled = true;
        int sum = 0;

        while (match.Success) {
            if (match.Value.Contains("mul") && isEnabled) {
                MatchCollection digits = Regex.Matches(match.Value, @"\d+");
                sum += int.Parse(digits[0].Value) * int.Parse(digits[1].Value);
            } else if (match.Value.Contains("don't()")) {
                isEnabled = false;
            } else if (match.Value.Contains("do()")) {
                isEnabled = true;
            }
            match = match.NextMatch();
        }
        return sum;
    }
}