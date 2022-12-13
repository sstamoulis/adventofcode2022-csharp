using Xunit;

namespace adventofcode2022.Tests;

public class Day12Tests {
    IEnumerable<string> sample = new string[] {
        "Sabqponm",
        "abcryxxl",
        "accszExk",
        "acctuvwj",
        "abdefghi",
    };

    [Fact]
    public void Part1Sample() {
        var d = new Day12(sample);
        Assert.Equal(31, d.Part1());
    }

    [Fact]
    public void Part1() {
        var d = new Day12();
        Assert.Equal(394, d.Part1());
    }

    [Fact]
    public void Part2sample() {
        var d = new Day12(sample);
        Assert.Equal(29, d.Part2());
    }

    [Fact]
    public void Part2() {
        var d = new Day12();
        Assert.Equal(388, d.Part2());
    }
}