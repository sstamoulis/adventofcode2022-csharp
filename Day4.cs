using System.ComponentModel.DataAnnotations.Schema;
namespace adventofcode2022;

public class Day4 : IDay
{
    IEnumerable<string> input = Utils.GetInputLines(4);
    // IEnumerable<string> input = Utils.GetSampleInputLines(4);

    class Assignment
    {
        public int start { get; private set; }
        public int end { get; private set; }


        public Assignment(string range)
        {
            var s = range.Split('-');
            start = int.Parse(s[0]);
            end = int.Parse(s[1]);
        }
    }

    class Pair
    {
        Assignment first;
        Assignment second;
        public Pair(string line)
        {
            var s = line.Split(',');
            first = new Assignment(s[0]);
            second = new Assignment(s[1]);
        }

        public bool IsFullyContained() =>
            (first.start >= second.start && first.end <= second.end) ||
            (second.start >= first.start && second.end <= first.end);

        public bool IsOverlapping() =>
            (first.end >= second.start && first.start <= second.end) ||
            (second.end >= first.start && second.start <= first.end);

    }

    public object Part1()
    {
        int count = 0;
        foreach (var line in input)
        {
            var pair = new Pair(line);
            if (pair.IsFullyContained())
                count++;
        }
        return count;
    }

    public object Part2()
    {
        int count = 0;
        foreach (var line in input)
        {
            var pair = new Pair(line);
            if (pair.IsOverlapping())
                count++;
        }
        return count;
    }
}