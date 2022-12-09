namespace adventofcode2022;

class Program {

    static void Main(string[] args) {
        Type[] days = new[] {
            typeof(Day1),
            typeof(Day2),
            typeof(Day3),
            typeof(Day4),
            typeof(Day5),
            typeof(Day6),
            typeof(Day7),
            typeof(Day8),
            typeof(Day9),
        };

        foreach (var day in days) {
            var instance = Activator.CreateInstance(day);
            if (instance is IDay dayInstance) {
                Console.WriteLine($"{day.Name} Part1: {dayInstance.Part1()}");
                Console.WriteLine($"{day.Name} Part2: {dayInstance.Part2()}");
            }
        }
    }
}
