using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day3 : ISolveable
{
    public async Task SolveAsync()
    {
        var input = await File.ReadAllTextAsync("Input/day3.txt"); // "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        
        // Part 1
        var mulRegex = MulRegex();
        var sum = mulRegex.Matches(input).Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

        // Part 2
        var conditionalMulRegex = ConditionalMulRegex();
        var active = true;
        var conditionalSum = conditionalMulRegex.Matches(input).Aggregate(0, (acc, match) =>
        {
            switch (match.Groups[0].Value)
            {
                case "do()":
                    active = true;
                    break;
                case "don't()":
                    active = false;
                    break;
                default:
                    if (active)
                        acc += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                    break;
            }

            return acc;
        });

        Console.WriteLine($"Day 3 P1: {sum}");
        Console.WriteLine($"Day 3 P2: {conditionalSum}");
    }

    [GeneratedRegex(@"(?:mul\((\d+),(\d+)\)|do\(\)|don't\(\))")]
    private static partial Regex ConditionalMulRegex();
    
    [GeneratedRegex(@"(?:mul\((\d+),(\d+)\))")]
    private static partial Regex MulRegex();
}