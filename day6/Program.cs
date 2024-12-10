namespace day6;

using System.Data;

class Program
{
    static void Main(string[] args)
    {
        string[] map = File.ReadLines("./input.txt").ToArray<string>();
        int positionCount1 = Part1(map);
        int positionCount2 = Part2(map);
        Console.WriteLine($"Part 1 answer: {positionCount1}");
        Console.WriteLine($"Part 2 answer: {positionCount2}");
    }

    private static int Part1(string[] map) {
        List<string> visited = [];
        string direction = "U";
        int x = map.Length, y = map[0].Length;
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[i].Length; j++) {
                if (map[i][j] == '^') {
                    visited.Add($"{i},{j}");
                    x = i;
                    y = j;
                }
            }
        }
        bool inBounds = x >= 0 && y >= 0 && x < map.Length && y < map[0].Length;
        while (inBounds) {
            switch (direction) {
                case "U":
                    if (x > 0 && map[x - 1][y] == '#') {
                        direction = "R";
                    } else {
                        x--;
                    }
                    break;
                case "D":
                    if (x < map.Length - 1 && map[x + 1][y] == '#') {
                        direction = "L";
                    } else {
                        x++;
                    }
                    break;
                case "L":
                    if (y > 0 && map[x][y - 1] == '#') {
                        direction = "U";
                    } else {
                        y--;
                    }
                    break;
                case "R":
                    if (y < map[0].Length - 1 && map[x][y + 1] == '#') {
                        direction = "D";
                    } else {
                        y++;
                    }
                    break;
            }
            inBounds = x >= 0 && y >= 0 && x < map.Length && y < map[0].Length;
            if (inBounds && !visited.Contains($"{x},{y}")) {
                visited.Add($"{x},{y}");
            }
        }
        return visited.Count;
    }

    private static int Part2(string[] map) {
        HashSet<string> loopingObstacles = [];
        Dictionary<string, List<string>> visited = new() {
            {"U", []},
            {"D", []},
            {"L", []},
            {"R", []}
        };
        string direction = "U";
        int x = map.Length, y = map[0].Length;
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[i].Length; j++) {
                if (map[i][j] == '^') {
                    x = i;
                    y = j;
                }
            }
        }
        bool inBounds = x >= 0 && y >= 0 && x < map.Length && y < map[0].Length;
        while (inBounds) {
            // Check if placing an obstacle on the current path will cause a loop
            int[] loopStart = [];
            string loopDirection = "";
            switch (direction) {
                case "U":
                    if (x > 0 && map[x - 1][y] != '#' && !visited[direction].Contains($"{x},{y}")) {
                        loopStart = [x - 1, y];
                        loopDirection = "R";
                    }
                    break;
                case "D":
                    if (x < map.Length - 1 && map[x + 1][y] != '#' && !visited[direction].Contains($"{x},{y}")) {
                        loopStart = [x + 1, y];
                        loopDirection = "L";
                    }
                    break;
                case "L":
                    if (y > 0 && map[x][y - 1] != '#' && !visited[direction].Contains($"{x},{y}")) {
                        loopStart = [x, y - 1];
                        loopDirection = "U";
                    }
                    break;
                case "R":
                    if (y < map[0].Length - 1 && map[x][y + 1] != '#' && !visited[direction].Contains($"{x},{y}")) {
                        loopStart = [x, y + 1];
                        loopDirection = "D";
                    }
                    break;
            }

            if (loopStart.Length > 0) {
                int i = x, j = y;
                bool loopBounds = i >= 0 && j >= 0 && i < map.Length && j < map[0].Length;
                bool foundLoop = false;
                Dictionary<string, List<string>> loopVisited = visited.ToDictionary(entry => entry.Key, entry => new List<string>(entry.Value));
                loopVisited[loopDirection].Add($"{i},{j}");
                while (loopBounds && !foundLoop) {
                    switch(loopDirection) {
                        case "U":
                            if (i > 0 && map[i - 1][j] == '#') {
                                loopDirection = "R";
                            } else {
                                i--;
                            }
                            break;
                        case "D":
                            if (i < map.Length - 1 && map[i + 1][j] == '#') {
                                loopDirection = "L";
                            } else {
                                i++;
                            }
                            break;
                        case "L":
                            if (j > 0 && map[i][j - 1] == '#') {
                                loopDirection = "U";
                            } else {
                                j--;
                            }
                            break;
                        case "R":
                            if (j < map[0].Length - 1 && map[i][j + 1] == '#') {
                                loopDirection = "D";
                            } else {
                                j++;
                            }
                            break;
                    }
                    if (loopVisited[loopDirection].Contains($"{i},{j}")) {
                        foundLoop = true;
                    } else {
                        loopVisited[loopDirection].Add($"{i},{j}");
                    }
                    loopBounds = i >= 0 && j >= 0 && i < map.Length && j < map[0].Length;
                }
                if (foundLoop) loopingObstacles.Add($"{loopStart[0]},{loopStart[1]}");
            }

            visited[direction].Add($"{x},{y}");
            switch (direction) {
                case "U":
                    if (x > 0 && map[x - 1][y] == '#') {
                        direction = "R";
                    } else {
                        x--;
                    }
                    break;
                case "D":
                    if (x < map.Length - 1 && map[x + 1][y] == '#') {
                        direction = "L";
                    } else {
                        x++;
                    }
                    break;
                case "L":
                    if (y > 0 && map[x][y - 1] == '#') {
                        direction = "U";
                    } else {
                        y--;
                    }
                    break;
                case "R":
                    if (y < map[0].Length - 1 && map[x][y + 1] == '#') {
                        direction = "D";
                    } else {
                        y++;
                    }
                    break;
            }
            inBounds = x >= 0 && y >= 0 && x < map.Length && y < map[0].Length;
        }
        Console.WriteLine(String.Join(" | ", loopingObstacles)); 
        return loopingObstacles.Count;
    }
}
