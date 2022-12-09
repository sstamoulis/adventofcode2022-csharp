using Xunit;

namespace adventofcode2022.Tests;

public class Day8Tests
{
    IEnumerable<string> sample = new string[] {
        "30373",
        "25512",
        "65332",
        "33549",
        "35390",
    };

    [Fact]
    public void Part1Sample()
    {
        var d = new Day8(sample);
        Assert.Equal(21, d.Part1());
    }

    [Fact]
    public void Part1()
    {
        var d = new Day8();
        Assert.Equal(1560, d.Part1());
    }

    [Fact]
    public void Part2Sample()
    {
        var d = new Day8(sample);
        Assert.Equal(8, d.Part2());
    }

    [Fact]
    public void Part2()
    {
        var d = new Day8();
        Assert.Equal(252000, d.Part2());
    }
}