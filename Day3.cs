namespace adventofcode2022;

public class Day3 : IDay
{
    IEnumerable<string> input = Utils.GetInputLines(3);

    int GetPriority(char c) => Char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27;

    public object Part1()
    {
        int sum = 0;
        foreach (var line in input)
        {
            var first = line.Take(line.Length / 2);
            var second = line.Skip(line.Length / 2);
            var common = first.Intersect(second).First();
            int priority = GetPriority(common);
            sum += priority;
        }
        return sum;
    }

    public object Part2()
    {
        int sum = 0;
        foreach (var group in input.Chunk(3))
        {
            var badge = group[0].Intersect(group[1]).Intersect(group[2]).First();
            var priority = GetPriority(badge);
            sum += priority;
        }
        return sum;
    }
}