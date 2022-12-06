namespace adventofcode2022;

public class Day1 : IDay {
    readonly IEnumerable<string> input;

    public Day1() {
        input = Utils.GetInputLines(1);
    }

    public Day1(IEnumerable<string> input) {
        this.input = input;
    }

    public object Part1() {
        var mostCalories = 0;
        var currentCalories = 0;
        foreach (var line in input) {
            if (String.IsNullOrWhiteSpace(line)) {
                if (currentCalories > mostCalories) {
                    mostCalories = currentCalories;
                }
                currentCalories = 0;
            } else {
                currentCalories += int.Parse(line);
            }
        }
        return mostCalories;
    }

    public object Part2() {
        int most1 = 0,
            most2 = 0,
            most3 = 0;

        int current = 0;
        foreach (var line in input.Concat(new string[] { "" })) {
            if (String.IsNullOrWhiteSpace(line)) {
                if (current > most1) {
                    most3 = most2;
                    most2 = most1;
                    most1 = current;
                } else if (current > most2) {
                    most3 = most2;
                    most2 = current;
                } else if (current > most3) {
                    most3 = current;
                }
                current = 0;
            } else {
                current += int.Parse(line);
            }
        }
        return most1 + most2 + most3;
    }
}