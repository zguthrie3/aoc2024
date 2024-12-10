class Program
{
    static void Main(string[] args)
    {
        int numSafe = Part1();
        int part2Safe = Part2();
        Console.WriteLine($"Part 1 answer: {numSafe}");
        Console.WriteLine($"Part 2 answer: {part2Safe}");
    }

    private static int Part1() {
        var lines = File.ReadLines("./input/input.txt");
        int safetyCount = 0;
        foreach (string line in lines) {
            int[] nums = Array.ConvertAll(line.Split(" "), s => int.Parse(s));
            bool isSafe = true, ascending = nums[1] > nums[0];
            Console.WriteLine($"Ascending? {ascending}");
            int i = 0;
            while (isSafe && i < nums.Length - 1) {
                if (
                    (ascending && (nums[i + 1] - nums[i] > 3 || nums[i + 1] - nums[i] < 1)) ||
                    (!ascending && (nums[i] - nums[i + 1] > 3 || nums[i] - nums[i + 1] < 1))
                ) {
                    isSafe = false;
                }
                i++;
            }
            if (isSafe) safetyCount++;
        }

        return safetyCount;
    }

    private static int Part2() {
        var lines = File.ReadLines("./input/input.txt");
        int safetyCount = 0;
        foreach (string line in lines) {
            int[] nums = Array.ConvertAll(line.Split(" "), s => int.Parse(s));
            bool isSafe = true, ascending = nums[1] > nums[0];
            const int TOLERANCE = 1;
            int i = 0, count = 0;
            while (isSafe && i < nums.Length - 1) {
                if (
                    (ascending && (nums[i + 1] - nums[i] > 3 || nums[i + 1] - nums[i] < 1)) ||
                    (!ascending && (nums[i] - nums[i + 1] > 3 || nums[i] - nums[i + 1] < 1))
                ) {
                    if (count < TOLERANCE) {
                        count++;
                    } else {
                        isSafe = false;
                    }
                }
                i++;
            }
            if (isSafe) safetyCount++;
        }

        return safetyCount;
    }
}
