using System.Security.Cryptography.X509Certificates;

namespace day1;

class Program
{
    static void Main(string[] args)
    {
        int sum = Part1();
        int similarityScore = Part2();
        Console.WriteLine($"Part 1 answer: {sum}");
        Console.WriteLine($"Part 2 answer: {similarityScore}");
    }

    private static int Part1() {
        var lines = File.ReadLines("./input/pt1.txt");
        List<int> left = [];
        List<int> right = [];
        foreach (string line in lines) {
            string[] nums = line.Split("   ");
            left.Add(Int32.Parse(nums[0]));
            right.Add(Int32.Parse(nums[1]));
        }
        left.Sort();
        right.Sort();
        int sum = 0;
        for (int i = 0; i < left.Count; i++) {
            sum += Math.Abs(right[i] - left[i]);
        }
        return sum;
    }

    private static int Part2() {
        List<int> left = [];
        Dictionary<int, int> rightCount = [];

        var lines = File.ReadLines("./input/pt1.txt");
        foreach (string line in lines) {
            string[] nums = line.Split("   ");
            left.Add(Int32.Parse(nums[0]));
            int right = Int32.Parse(nums[1]);
            if (!rightCount.TryAdd(right, 1)) {
                rightCount[right] += 1;
            }
        }
        int similarityScore = 0;

        foreach(int key in left) {
            if (rightCount.TryGetValue(key, out int value)) {
                similarityScore += key * value;
            }
        }
        return similarityScore;
    }
}
