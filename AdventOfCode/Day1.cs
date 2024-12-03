namespace AdventOfCode;

public class Day1 : ISolveable
{
    public async Task SolveAsync()
    {
        var left = new List<int>();
        var right = new List<int>();
        var input = await File.ReadAllTextAsync("Input/day1.txt");
        foreach (var line in input.Split("\n"))
        {
            var numbers = line.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray();
            left.Add(numbers[0]);
            right.Add(numbers[1]);
        }
        
        left.Sort();
        right.Sort();

        // Part 1
        var distance = left.Select((l, i) => Math.Abs(l - right[i])).Sum();

        // Part 2
        var rightOccurrences = CountOccurrences(right);
        var similarityScore = left.Sum(n => n * rightOccurrences.GetValueOrDefault(n));
        
        Console.WriteLine($"Day 1 distance: {distance}");
        Console.WriteLine($"Day 1 similarity: {similarityScore}");
    }

    private Dictionary<int, int> CountOccurrences(List<int> numbers)
    {
        var dict = new Dictionary<int, int>();
        
        numbers.ForEach(n =>
        {
            if (!dict.TryAdd(n, 1))
            {
                dict[n]++;
            }
        });

        return dict;
    }
}