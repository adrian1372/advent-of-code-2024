namespace AdventOfCode;

public class Day4 : ISolveable
{
    public async Task SolveAsync()
    {
        // const string input = """
        //                      MMMSXXMASM
        //                      MSAMXMSMSA
        //                      AMXSXMAAMM
        //                      MSAMASMSMX
        //                      XMASAMXAMM
        //                      XXAMMXXAMA
        //                      SMSMSASXSS
        //                      SAXAMASAAA
        //                      MAMMMXMMMM
        //                      MXMXAXMASX
        //                      """;

        var input = await File.ReadAllTextAsync("Input/day4.txt");
        
        var inputMatrix = input.Split('\n');

        var maxLineX = inputMatrix[0].Length;
        var maxLineY = inputMatrix.Length;

        var numXmas = inputMatrix
            .SelectMany((line, y) => line.Select((c, x) => new {c, x, y}))
            .Where(x => x.c is 'X')
            .SelectMany(x =>
            {
                // Get indices to check
                List<string> list = [];

                var canGoLeft = x.x >= 3;
                var canGoRight = x.x < maxLineX - 3;
                var canGoUp = x.y >= 3;
                var canGoDown = x.y < maxLineY - 3;

                if (canGoRight)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, true, false, true, false));

                if (canGoLeft)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, true, false, false, false));

                if (canGoDown)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, false, true, false, true));

                if (canGoUp)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, false, true, false, false));

                if (canGoDown && canGoRight)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, true, true, true, true));

                if (canGoDown && canGoLeft)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, true, true, false, true));

                if (canGoUp && canGoRight)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, true, true, true, false));

                if (canGoUp && canGoLeft)
                    list.Add(GenerateString(inputMatrix, x.x, x.y, true, true, false, false));

                return list;
            })
            .Count(str => str == "XMAS");
        
        Console.WriteLine($"Day 4 P1: {numXmas}");
        Console.WriteLine($"Day 4 P2: {SolvePart2(inputMatrix)}");
    }

    public string GenerateString(string[] input, int startX, int startY, bool moveX, bool moveY,
        bool xAsc, bool yAsc) => new(Enumerable.Range(0, 4).Select(i => input
            [moveY ? startY + i * (yAsc ? 1 : -1) : startY]
            [moveX ? startX + i * (xAsc ? 1 : -1) : startX]
        ).ToArray());
    
    private bool TestPath(string[] input, IReadOnlyCollection<(int x, int y)> path) => 
        input[path.ElementAt(0).y][path.ElementAt(0).x] == 'X'
        && input[path.ElementAt(1).y][path.ElementAt(1).x] == 'M'
        && input[path.ElementAt(2).y][path.ElementAt(2).x] == 'A'
        && input[path.ElementAt(3).y][path.ElementAt(3).x] == 'S';

    private int SolvePart2(string[] input)
    {
        var maxX = input.Length - 1;
        var maxY = input[0].Length - 1;

        string[] allowedPatterns =
        [
            "MSMS",
            "MMSS",
            "SSMM",
            "SMSM"
        ];
        
        return input
            .SelectMany((line, y) => line.Select((c, x) => new {c, x, y}).Where(x => x.c is 'A'))
            .Where(x => x.x >= 1 && x.x <= maxX - 1 && x.y >= 1 && x.y <= maxY - 1)
            .Select(x => (x.x, x.y))
            .Select(coord => new string(new [] {input[coord.y - 1][coord.x - 1], input[coord.y - 1][coord.x + 1], input[coord.y + 1][coord.x - 1], input[coord.y + 1][coord.x + 1]}))
            .Count(str => allowedPatterns.Contains(str));
            
    }
}