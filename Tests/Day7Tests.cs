using Xunit;

namespace adventofcode2022.Tests;

public class Day7Tests {
    readonly string[] sample = new string[] {
        "$ cd /",
        "$ ls",
        "dir a",
        "14848514 b.txt",
        "8504156 c.dat",
        "dir d",
        "$ cd a",
        "$ ls",
        "dir e",
        "29116 f",
        "2557 g",
        "62596 h.lst",
        "$ cd e",
        "$ ls",
        "584 i",
        "$ cd ..",
        "$ cd ..",
        "$ cd d",
        "$ ls",
        "4060174 j",
        "8033020 d.log",
        "5626152 d.ext",
        "7214296 k",
    };

    [Fact]
    public void Part1Sample() {
        var d = new Day7(sample);
        Assert.Equal(95437, d.Part1());
    }

    [Fact]
    public void Part1() {
        var d = new Day7();
        Assert.Equal(1543140, d.Part1());
    }

    [Fact]
    public void Part2Sample() {
        var d = new Day7(sample);
        Assert.Equal(24933642, d.Part2());
    }

    [Fact]
    public void Part2() {
        var d = new Day7();
        Assert.Equal(1117448, d.Part2());
    }

}