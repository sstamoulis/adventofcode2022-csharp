namespace adventofcode2022;

public class Day6 : IDay
{
    string buffer = Utils.GetInputLines(6).First();

    bool HasDuplicate(int start, int count)
    {
        for (var i = start; i > start - count + 1; i--)
            for (var j = i - 1; j > start - count; j--)
                if (buffer[i] == buffer[j]) return true;
        return false;
    }

    public object Part1()
    {
        int i;
        for (i = 3; i < buffer.Length; i++)
            if (!HasDuplicate(i, 4)) break;
        return i + 1;
    }

    public object Part2()
    {
        int i;
        for (i = 13; i < buffer.Length; i++)
            if (!HasDuplicate(i, 14)) break;
        return i + 1;
    }
}