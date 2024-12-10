// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        int xmasCount = Part1();
        int xmas2Count = Part2();
        Console.WriteLine($"Part 1 answer: {xmasCount}");
        Console.WriteLine($"Part 2 answer: {xmas2Count}");
    }

    private static int Part1() {
        int count = 0;
        string[] rows = File.ReadLines("./input.txt").ToArray<string>();
        for (int i = 0; i < rows.Length; i++) {
            for (int j = 0; j < rows[i].Length; j++) {
                // Console.WriteLine($"X, Y: {i}, {j}");
                if (rows[i][j] == 'X') {
                    if (i >= 3) {
                        if (j >= 3) {
                            count += rows[i - 1][j - 1] == 'M' && 
                                    rows[i - 2][j - 2] == 'A' &&
                                    rows[i - 3][j - 3] == 'S' ? 1 : 0;
                        }
                        if (j < rows[i].Length - 3) {
                            count += rows[i - 1][j + 1] == 'M' && 
                                    rows[i - 2][j + 2] == 'A' &&
                                    rows[i - 3][j + 3] == 'S' ? 1 : 0;
                        }
                        count += rows[i - 1][j] == 'M' && 
                                    rows[i - 2][j] == 'A' &&
                                    rows[i - 3][j] == 'S' ? 1 : 0;
                    }

                    if (i < rows[i].Length - 3) {
                        if (j >= 3) {
                            count += rows[i + 1][j - 1] == 'M' && 
                                    rows[i + 2][j - 2] == 'A' &&
                                    rows[i + 3][j - 3] == 'S' ? 1 : 0;
                        }
                        if (j < rows[i].Length - 3) {
                            count += rows[i + 1][j + 1] == 'M' && 
                                    rows[i + 2][j + 2] == 'A' &&
                                    rows[i + 3][j + 3] == 'S' ? 1 : 0;
                        }
                        count += rows[i + 1][j] == 'M' && 
                                    rows[i + 2][j] == 'A' &&
                                    rows[i + 3][j] == 'S' ? 1 : 0;
                    }

                    if (j >= 3) {
                        count += rows[i][j - 1] == 'M' && 
                                rows[i][j - 2] == 'A' &&
                                rows[i][j - 3] == 'S' ? 1 : 0;
                    }
                    if (j < rows[i].Length - 3) {
                        count += rows[i][j + 1] == 'M' && 
                                rows[i][j + 2] == 'A' &&
                                rows[i][j + 3] == 'S' ? 1 : 0;
                    }
                }
            }
        }
        return count;
    }

    private static int Part2() {
        int count = 0;
        string[] rows = File.ReadLines("./input.txt").ToArray<string>();
        for (int i = 0; i < rows.Length; i++) {
            for (int j = 0; j < rows[i].Length; j++) {
                if (rows[i][j] == 'A') {
                    if (i > 0 && i < rows.Length - 1 && j > 0 && j < rows[i].Length - 1) {
                        char upperLeft = rows[i - 1][j - 1];
                        if (upperLeft == 'S') {
                            bool lowerPartner = rows[i + 1][j - 1] == upperLeft &&
                                                rows[i + 1][j + 1] == 'M' &&
                                                rows[i - 1][j + 1] == 'M';
                            bool upperPartner = rows[i - 1][j + 1] == upperLeft &&
                                                rows[i + 1][j + 1] == 'M' &&
                                                rows[i + 1][j - 1] == 'M';
                            count += lowerPartner || upperPartner ? 1 : 0;
                        }

                        if (upperLeft == 'M') {
                            bool lowerPartner = rows[i + 1][j - 1] == upperLeft &&
                                                rows[i + 1][j + 1] == 'S' &&
                                                rows[i - 1][j + 1] == 'S';
                            bool upperPartner = rows[i - 1][j + 1] == upperLeft &&
                                                rows[i + 1][j + 1] == 'S' &&
                                                rows[i + 1][j - 1] == 'S';
                            count += lowerPartner || upperPartner ? 1 : 0;
                        }
                    }
                }
            }
        }
        return count;
    }
}
