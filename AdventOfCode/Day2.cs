namespace AdventOfCode;

public class Day2 : ISolveable
{
    public async Task SolveAsync()
    {
        var input = (await File.ReadAllTextAsync("Input/day2.txt")).Split('\n').Select(r => r.Split(' ').Select(int.Parse).ToList()).ToList();

        var numSafe = input.Count(report => IsSafe(report, false));
        var numSafePart2 = input.Count(report => IsSafe(report, true));
        
        Console.WriteLine($"Day 2 P1: {numSafe}");
        Console.WriteLine($"Day 2 P2: {numSafePart2}");
    }

    private bool IsSafe(List<int> report, bool canUseProblemDampener = true)
    {
        if (report is {Count: 0 or 1})
        {
            Console.WriteLine("Safe");
            return true;
        }
        var ascending = report[0] < report[1];

        for (var i = 0; i < report.Count - 1; i++)
        {
            if (IsSafe(report[i], report[i + 1], ascending))
                continue;

            if (!canUseProblemDampener)
                return false;

            return i == report.Count - 2 || ReportIsSafeWithProblemDampener(report);
        }

        return true;
    }

    private bool ReportIsSafeWithProblemDampener(List<int> report)
    {
        for (var i = 0; i < report.Count - 1; i++)
        {
            var copy = new List<int>(report);
            copy.RemoveAt(i);

            if (IsSafe(copy, false)) return true;
        }

        return false;
    }
    
    private bool IsSafe(int a, int b, bool ascending) => ascending
        ? b > a && b <= a + 3
        : b >= a - 3 && b < a;
}