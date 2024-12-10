namespace day8;

class Program
{
    static void Main(string[] args)
    {
        string[] map = File.ReadLines("./input.txt").ToArray<string>();
        int antinodeCount1 = Part1(map);
        int antinodeCount2 = Part2(map);
        Console.WriteLine($"Part 1 answer: {antinodeCount1}");
        Console.WriteLine($"Part 2 answer: {antinodeCount2}");
    }

    private static int Part1(string[] map) {
        HashSet<string> antinodes = [];
        Dictionary<char, List<int[]>> charMap = [];
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[0].Length; j++) {
                char tile = map[i][j];
                if (tile != '.') {
                    if (!charMap.ContainsKey(tile)) {
                        charMap.Add(tile, [[i, j]]);
                    } else {
                        charMap[tile].Add([i, j]);
                    }
                }
            }
        }

        foreach(KeyValuePair<char, List<int[]>> entry in charMap) {
            for(int i = 0; i < entry.Value.Count; i++) {
                int[] current = entry.Value[i];
                for (int j = i + 1; j < entry.Value.Count; j++) {
                    int[] paired = entry.Value[j];
                    int[] diff = [paired[0] - current[0], paired[1] - current[1]];
                    int[] antinode1 = [paired[0] + diff[0], paired[1] + diff[1]];
                    int[] antinode2 = [current[0] - diff[0], current[1] - diff[1]];

                    if (antinode1[0] >= 0 && antinode1[1] >= 0 && antinode1[0] < map.Length && antinode1[1] < map[0].Length) {
                        antinodes.Add($"{antinode1[0]},{antinode1[1]}");
                    }

                    if (antinode2[0] >= 0 && antinode2[1] >= 0 && antinode2[0] < map.Length && antinode2[1] < map[0].Length) {
                        antinodes.Add($"{antinode2[0]},{antinode2[1]}");
                    }
                }
            }
        }
        Console.WriteLine(String.Join(" | ", antinodes)); 
        return antinodes.Count;
    }

    private static int Part2(string[] map) {
        HashSet<string> antinodes = [];
        Dictionary<char, List<int[]>> charMap = [];
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[0].Length; j++) {
                char tile = map[i][j];
                if (tile != '.') {
                    if (!charMap.ContainsKey(tile)) {
                        charMap.Add(tile, [[i, j]]);
                    } else {
                        charMap[tile].Add([i, j]);
                    }
                }
            }
        }

        foreach(KeyValuePair<char, List<int[]>> entry in charMap) {
            for(int i = 0; i < entry.Value.Count; i++) {
                int[] current = entry.Value[i];
                antinodes.Add($"{current[0]},{current[1]}");
                for (int j = i + 1; j < entry.Value.Count; j++) {
                    int[] temp = current;
                    int[] paired = entry.Value[j];
                    antinodes.Add($"{paired[0]},{paired[1]}");
                    int[] diff = [paired[0] - temp[0], paired[1] - temp[1]];
                    bool inBounds = true;
                    while(inBounds) {
                        int[] antinode1 = [paired[0] + diff[0], paired[1] + diff[1]];
                        if (antinode1[0] >= 0 && antinode1[1] >= 0 && antinode1[0] < map.Length && antinode1[1] < map[0].Length) {
                            antinodes.Add($"{antinode1[0]},{antinode1[1]}");
                            paired = antinode1;
                        } else {
                            inBounds = false;
                        }
                    }

                    inBounds = true;
                    while(inBounds) {
                        int[] antinode2 = [temp[0] - diff[0], temp[1] - diff[1]];
                        if (antinode2[0] >= 0 && antinode2[1] >= 0 && antinode2[0] < map.Length && antinode2[1] < map[0].Length) {
                            antinodes.Add($"{antinode2[0]},{antinode2[1]}");
                            temp = antinode2;
                        } else {
                            inBounds = false;
                        }
                    }
                }
            }
        }
        Console.WriteLine(String.Join(" | ", antinodes)); 
        return antinodes.Count;
    }
}
