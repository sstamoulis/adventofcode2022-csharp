using Xunit;

namespace adventofcode2022.Tests;

public class Day1Tests {

    readonly static string[] sample = new string[] {
        "1000",
        "2000",
        "3000",
        "",
        "4000",
        "",
        "5000",
        "6000",
        "",
        "7000",
        "8000",
        "9000",
        "",
        "10000",
    };

    [Fact]
    public void Part1Sample() {
        var d = new Day1(sample);
        Assert.Equal(24000, d.Part1());
    }

    [Fact]
    public void Part1() {
        var d = new Day1();
        Assert.Equal(70369, d.Part1());
    }

    [Fact]
    public void Part2Sample() {
        var d = new Day1(sample);
        Assert.Equal(45000, d.Part2());
    }

    [Fact]
    public void Part2() {
        var d = new Day1();
        Assert.Equal(203002, d.Part2());
    }
}