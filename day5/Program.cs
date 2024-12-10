using System.Collections;
using System.Linq.Expressions;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllText("./input.txt").Split(@"\n\r\n");
        Console.WriteLine($"Input[0]: {input[0]}");
        List<string> orderPairs = input[0].Split(@"\n").Select(pair => pair.Trim()).ToList<string>();
        Dictionary<string, List<string>> rules = generateRuleMapping(orderPairs);
        List<string> instructions = input[1].Split(@"\n").Select(i => i.Trim()).ToList<string>();
        int day1Count = Part1(rules, instructions);
        int day2Count = Part2(rules, instructions);
        Console.WriteLine($"Part 1 answer: {day1Count}");
        Console.WriteLine($"Part 2 answer: {day2Count}");
    }

    private static Dictionary<string, List<string>> generateRuleMapping(List<string> orderPairs) {
        Dictionary<string, List<string>> rules = [];
        foreach (string orderPair in orderPairs) {
            string[] pair = orderPair.Split("|");
            Console.WriteLine($"Order Pair: {orderPair}\tPair 0? {pair[0]}");
            List<string> ruleList = rules.ContainsKey(pair[0]) ? rules[pair[0]] : [];
            ruleList.Add(pair[1].Trim());
            rules.Add(pair[0], ruleList);
        }
        return rules;
    }

    private static int Part1(Dictionary<string, List<string>> rules, List<string> instructions) {
        int score = 0;
        foreach (string instruction in instructions) {
            string[] pages = instruction.Split(",");
            int i = 0;
            bool isValid = true;
            HashSet<string> seen = [];
            while (isValid && i < pages.Length) {
                string page = pages[i];
                List<string> pageRules = rules[page];
                if (pageRules != null) {
                    foreach (string prev in seen) {
                        if (pageRules.Contains(prev)) isValid = false;
                    }
                }
                seen.Add(page.Trim());
                i++;
            }
            if (isValid) score += int.Parse(pages[pages.Length / 2]);
        }
        return score;
    }

    private static int Part2(Dictionary<string, List<string>> rules, List<string> instructions) {
        List<List<string>> repaired = [];
        foreach (string instruction in instructions) {
            string[] pages = instruction.Split(",");
            List<string> adjusted = [.. pages];
            bool isValid = true;
            HashSet<string> seen = [];
            for (int i = 0; i < pages.Length; i++) {
                string page = pages[i];
                List<string> pageRules = rules[page];
                if (pageRules != null) {
                    int earliest = int.MaxValue;
                    foreach (string prev in seen) {
                        if (pageRules.Contains(prev)) {
                            if (isValid) isValid = false;
                            earliest = earliest < adjusted.IndexOf(prev) ? earliest : adjusted.IndexOf(prev);
                        }
                    }
                    if (earliest < int.MaxValue) {
                        adjusted.Insert(earliest, page);
                        adjusted.RemoveAt(i + 1);
                    }
                }
                seen.Add(page.Trim());
            }
            if (!isValid) repaired.Add(adjusted);
        }
        return repaired.Select(str => int.Parse(str[str.Count / 2])).Sum();
    }
}