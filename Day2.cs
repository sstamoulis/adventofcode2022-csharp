using System.Text;

namespace adventofcode2022;

#pragma warning disable CS8524

public class Day2 : IDay
{
    public IEnumerable<string> input;

    public Day2()
    {
        input = Utils.GetInputLines(2);
    }

    const int SCORE_WIN = 6;
    const int SCORE_DRAW = 3;
    const int SCORE_LOSS = 0;

    enum Hand
    {
        Rock,
        Paper,
        Scissors
    }

    enum Result
    {
        Loss,
        Draw,
        Win
    }

    static Hand GetWinning(Hand hand)
    {
        return hand switch
        {
            Hand.Rock => Hand.Paper,
            Hand.Paper => Hand.Scissors,
            Hand.Scissors => Hand.Rock
        };
    }

    static Hand GetLosing(Hand hand)
    {
        return hand switch
        {
            Hand.Rock => Hand.Scissors,
            Hand.Paper => Hand.Rock,
            Hand.Scissors => Hand.Paper
        };
    }

    static Result GetResult(Hand self, Hand opponent)
    {
        if (self == opponent)
            return Result.Draw;
        else if (GetWinning(self) == opponent)
            return Result.Loss;
        else
            return Result.Win;
    }

    static int GetScore(Result result) =>
        result switch
        {
            Result.Win => SCORE_WIN,
            Result.Draw => SCORE_DRAW,
            Result.Loss => SCORE_LOSS
        };

    public object Part1()
    {
        var total = 0;
        foreach (var line in input)
        {
            var opponent = (Hand)(line[0] - 'A');
            var self = (Hand)(line[2] - 'X');
            var result = GetResult(self, opponent);
            total += (int)self + 1 + GetScore(result);
        }
        return total;
    }

    public object Part2()
    {
        var total = 0;
        foreach (var line in input)
        {
            var opponent = (Hand)(line[0] - 'A');
            var result = (Result)(line[2] - 'X');
            var self = result switch
            {
                Result.Win => GetWinning(opponent),
                Result.Draw => opponent,
                Result.Loss => GetLosing(opponent)
            };
            total += (int)self + 1 + GetScore(result);
        }
        return total;
    }
}