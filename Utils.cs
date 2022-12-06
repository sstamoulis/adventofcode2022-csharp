namespace adventofcode2022;

public static class Utils
{
    public static IEnumerable<string> GetInputLines(int day)
    {
        var file = $"input/day{day}.txt";
        return File.ReadLines(file);
    }

    public static IEnumerable<string> GetSampleInputLines(int day)
    {
        var file = $"input/sample{day}.txt";
        return File.ReadLines(file);
    }
}