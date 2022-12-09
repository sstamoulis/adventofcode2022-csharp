using Xunit;

namespace adventofcode2022.Tests;

public class Day9Tests
{

    [Fact]
    public void Part1Sample()
    {
        IEnumerable<string> sample = new string[] {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2",
        };
        var d = new Day9(sample);
        Assert.Equal(13, d.Part1());
    }

    [Fact]
    public void Part1()
    {
        var d = new Day9();
        Assert.Equal(6376, d.Part1());
    }

    [Fact]
    public void Part2Sample()
    {
        IEnumerable<string> sample = new string[] {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20",
        };
        var d = new Day9(sample);
        Assert.Equal(36, d.Part2());
    }
    
    [Fact]
    public void Part2()
    {
        var d = new Day9();
        Assert.Equal(2607, d.Part2());
    }
}